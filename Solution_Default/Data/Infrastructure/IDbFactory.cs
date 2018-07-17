using System;

namespace Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        //phương thưc giao tiếp khởi tạo đối tượng
        DBContext Init();
    }
}