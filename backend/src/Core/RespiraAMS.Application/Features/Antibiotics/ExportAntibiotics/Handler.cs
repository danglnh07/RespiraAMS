using System.Diagnostics;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RespiraAMS.Application.Abstracts.CQRS;
using RespiraAMS.Application.Abstracts.Data;
using RespiraAMS.Domain.Enums;

namespace RespiraAMS.Application.Features.Antibiotics.ExportAntibiotics;

public class ExportAntibioticsHandler(IDbContext context, ILogger<ExportAntibioticsHandler> logger) 
    : ICommandHandler<ExportAntibioticsCommand, byte[]>
{
    private static string GetRouteOfAdministrationName(RouteOfAdministration route)
    {
        return route switch
        {
            RouteOfAdministration.Oral => "Đường uống",
            RouteOfAdministration.Intravenous => "Đường tĩnh mạch",
            _ => ""
        };
    }

    private static string GetAWaReCategoryName(AwareCategory category)
    {
        return category switch
        {
            AwareCategory.AccessWatch => "Access-Watch",
            _ => category.ToString()
        };
    }
    
    private static void StyleDosageCell(IXLCell cell, Dictionary<RouteOfAdministration, List<string>> dosages)
    {
        var richText = cell.GetRichText();

        var count = 1;
        foreach (var (route, values) in dosages)
        {
            richText.AddText($"[{GetRouteOfAdministrationName(route)}]")
                .SetBold()
                .SetFontColor(XLColor.Blue);
            
            // Dosages
            richText.AddText($": {string.Join("; ", values)}");

            // New line between routes
            if (count < dosages.Count)
            {
                richText.AddNewLine();
            }
            count++;
        }

        cell.Style.Alignment.WrapText = true;
    }
    
    public async Task<byte[]> HandleAsync(ExportAntibioticsCommand command)
    {
        var sw = Stopwatch.StartNew();
        // Get all antibiotics
        var antibiotics = await context.Antibiotics
            .Include(x => x.AntibioticSpectrum)
            .ToListAsync();

        // Create worksheet
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Antibiotics");
        
        // Create table header
        worksheet.Cell("A1").Value = "Tên";
        worksheet.Cell("B1").Value = "Phổ kháng sinh";
        worksheet.Cell("C1").Value = "Liều thường dùng";
        worksheet.Cell("D1").Value = "AWaRe";
        var header = worksheet.Range("A1:D1");
        header.Style.Font.Bold = true;
        header.Style.Fill.BackgroundColor = XLColor.LightGray;
        header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        header.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

        // Style for all rows
        worksheet.Rows().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        
        // Insert data
        var row = 2;
        foreach (var antibiotic in antibiotics)
        {
            worksheet.Cell($"A{row}").Value = antibiotic.Name;
            worksheet.Cell($"B{row}").Value = antibiotic.AntibioticSpectrum.Name;
            worksheet.Cell($"C{row}").Value = GetAWaReCategoryName(antibiotic.Category);
            StyleDosageCell(worksheet.Cell($"D{row}"), antibiotic.Dosages);
            row++;
        }
        
        // Style for specific columns
        worksheet.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Column("B").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Column("C").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Columns().AdjustToContents();

        using var result = new MemoryStream();
        workbook.SaveAs(result);

        sw.Stop();
        logger.LogInformation("Export antibiotics success in {millis} ms", sw.ElapsedMilliseconds);
        
        return result.ToArray();
    }
}