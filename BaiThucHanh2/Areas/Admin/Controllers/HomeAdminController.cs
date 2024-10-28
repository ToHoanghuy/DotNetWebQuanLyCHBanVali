using Azure;
using BaiThucHanh2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BaiThucHanh2.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]

    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db= new QlbanVaLiContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham (int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemSanPhamMoi")]
        [HttpGet]

        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(),"MaChatLieu","ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid) {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);

            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View(sanPham);
        }

        [Route("XoaSanPham")]
        [HttpGet]

        public IActionResult XoaSanPham (string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPhams = db.TChiTietSanPhams.Where(x=>x.MaSp==maSanPham);
            if (chiTietSanPhams.Count() > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSanPhams=db.TAnhSps.Where(x=>x.MaSp==maSanPham).ToList();
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin"); ;
        }

        [Route("quanlynhanvien")]
        public IActionResult QuanLyNhanVien(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var lstnhanvien = db.TNhanViens.AsNoTracking().OrderBy(x => x.TenNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(lstnhanvien, pageNumber, pageSize);
            return View(lst);
        }

        /*[Route("danhsachnguoidung")]
        public IActionResult DanhSachNguoiDung(int? page)
        {
            int pageSize = 16;
            int pageIndex = page == null ? 1 : page.Value;
            var lst = db.TNhanViens.ToList();
            PagedList<TNhanVien> listUsr = new PagedList<TNhanVien>(lst, pageIndex, pageSize);
            return View(listUsr);
        }*/

        [Route("themnguoidungmoi")]
        [HttpGet]
        public IActionResult ThemNguoiDungMoi()
        {
            // ViewBag.MaChatLieu = new SelectList(db.TChatLieus, "MaChatLieu", "ChatLieu");
            // ViewBag.MaHangSx = new SelectList(db.THangSxes, "MaHangSx", "HangSx");
            // ViewBag.MaNuocSx = new SelectList(db.TQuocGia, "MaNuoc", "TenNuoc");
            // ViewBag.MaLoai = new SelectList(db.TLoaiSps, "MaLoai", "Loai");
            // ViewBag.MaDt = new SelectList(db.TLoaiDts, "MaDt", "TenLoai");

            return View();
        }
        [Route("themnguoidungmoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNguoiDungMoi(TNhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachNguoiDung");
            }
            return View(nhanVien);
        }

        [Route("xoanguoidung/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult XoaNguoiDung(string id)
        {
            // Tìm người dùng theo id
            var nhanVien = db.TNhanViens.Find(id);

            if (nhanVien == null)
            {
                // Nếu không tìm thấy, có thể thêm thông báo lỗi ở đây
                return NotFound(); // Hoặc bạn có thể redirect về một trang khác
            }

            db.TNhanViens.Remove(nhanVien); // Xóa người dùng
            db.SaveChanges(); // Lưu thay đổi

            return RedirectToAction("QuanlyNhanVien"); // Chuyển hướng đến danh sách người dùng
        }



    }


}
