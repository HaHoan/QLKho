using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Databases
{
   public abstract class DbQuery
    {

        /// <summary>
        /// Lấy hết dữ liệu từ 1 bảng
        /// </summary>
        /// <returns> Danh sách record đã được chuyển thành object</returns>
        public abstract IEnumerable<object> Select();

        /// <summary>
        /// Chèn dữ liệu vào bảng
        /// </summary>
        /// <param name="o"> Dữ liệu cần chèn</param>
        /// <returns>Dòng đã chèn có bao gồm Id sinh tự động</returns>
        public abstract object Insert(object o);

        /// <summary>
        /// Cập nhật dòng từ bảng
        /// </summary>
        /// <param name="o"> Dữ liệu cần cập nhật</param>
        /// <returns> Số dòng được cập nhật</returns>
        public abstract int Update(object o);

        /// <summary>
        /// Xóa dòng từ bảng
        /// </summary>
        /// <param name="o"> Đối tượng cần xóa</param>
        /// <returns> Số dòng được xóa</returns>
        public abstract int Delete(object o);

    }
}
