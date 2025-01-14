using System.Net;

namespace Dot_Net_ECommerceWeb.Service
{
    public class ServiceIPAddress
    {
        public static string ConvertToIPv4(string ipv6Address)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ipv6Address);
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    // Kiểm tra nếu là loopback (::1) và trả về localhost IPv4
                    if (IPAddress.IsLoopback(ipAddress))
                    {
                        return "127.0.0.1";
                    }

                    // Chuyển đổi IPv6 về IPv4 (nếu áp dụng)
                    byte[] bytes = ipAddress.GetAddressBytes();
                    if (bytes.Length >= 16) // IPv6 có 16 byte
                    {
                        return BytesToIPv4String(bytes, 12, 4);
                    }
                }

                // Trường hợp không phải IPv6, trả về trực tiếp
                return ipv6Address;
            }
            catch (FormatException)
            {
                // Xử lý khi định dạng IP không hợp lệ
                return "unknown";
            }
        }

        /// <summary>
        /// Chuyển đổi mảng byte thành địa chỉ IPv4.
        /// </summary>
        /// <param name="bytes">Mảng byte.</param>
        /// <param name="offset">Vị trí bắt đầu trong mảng byte.</param>
        /// <param name="length">Số byte cần đọc.</param>
        /// <returns>Địa chỉ IPv4 dưới dạng chuỗi.</returns>
        private static string BytesToIPv4String(byte[] bytes, int offset, int length)
        {
            var segments = new string[length];
            for (int i = 0; i < length; i++)
            {
                segments[i] = (bytes[offset + i] & 0xFF).ToString();
            }
            return string.Join('.', segments);
        }

    }
}

