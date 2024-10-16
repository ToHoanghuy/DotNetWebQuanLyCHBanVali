using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BaiThucHanh2.Models;
using BaiThucHanh2.Models.ProductModels;

namespace BaiThucHanh2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanVaLiContext db=new QlbanVaLiContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham=(from p in db.TDanhMucSps select new Product
            {
                Masp=p.MaSp,
                TenSp=p.TenSp,
                MaLoai=p.MaLoai,
                AnhDaiDien=p.AnhDaiDien,
                GiaNhoNhat=p.GiaNhoNhat,
            }).ToList();
            return sanPham;
        }
        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetProductByCategory(string maloai)
        {
            var sanPham = (from p in db.TDanhMucSps
                           where p.MaLoai==maloai
                           select new Product
                           {
                               Masp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaNhoNhat = p.GiaNhoNhat,
                           }).ToList();
            return sanPham;
        }
    }
}
