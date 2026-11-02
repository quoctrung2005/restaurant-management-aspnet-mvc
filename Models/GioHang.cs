using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA_LTW.Models
{
    public class GioHang
    {
        QL_NHAHANGEntities db = new QL_NHAHANGEntities();

        public string iMaMon { get; set; } 

        public string sTenMon { get; set; }
        public string sHinhAnh { get; set; }
        public int dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public int dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        public GioHang(string MaMon)
        {
            iMaMon = MaMon;
            MONAN mon = db.MONANs.Single(n => n.MaMon == iMaMon);

            sTenMon = mon.TenMon;
            sHinhAnh = mon.HinhAnh;
            dDonGia = (int)mon.Gia;
            iSoLuong = 1;
        }
    }
}