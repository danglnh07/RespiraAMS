namespace RespiraAMS.Core.Enums;

/*
 * Vì đường dùng thuốc gần như là cố định -> enum sẽ hợp lý hơn là 1 bảng riêng
 */

/// <summary>
/// Đường dùng thuốc
/// </summary>
public enum RouteOfAdministration
{
    Oral, // Đường uống
    Intravenous, // Đường tiêm tĩnh mạch
}