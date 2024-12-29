using Microsoft.Extensions.Caching.Memory;
using System;

namespace PhongMachTu.Common.Helpers
{
    public class TokenStore
    {
        private readonly IMemoryCache _cache;

        public TokenStore(IMemoryCache cache)
        {
            _cache = cache;
        }

        // Thêm token mới (thay thế token cũ nếu tồn tại)
        public void AddToken( int userId, string token)
        {
            // Lưu token vào cache, mỗi UserId chỉ có một token
            _cache.Set(userId, token);
        }

        // Xóa token theo UserId (nếu không có thì thôi k ném ra ngoại lệ)
        public void RevokeToken(int userId)
        {
            _cache.Remove(userId);
        }

        // Kiểm tra token có hợp lệ không
        public bool IsTokenValid(string token, int userId)
        {
            if (_cache.TryGetValue(userId, out string cachedToken))
            {
                return cachedToken == token;
            }
            return false;
        }

        // Lấy token hiện tại của người dùng
        public string? GetTokenByUserId(int userId)
        {
            if (_cache.TryGetValue(userId, out string cachedToken))
            {
                return cachedToken;
            }
            return null;
        }
    }
}
