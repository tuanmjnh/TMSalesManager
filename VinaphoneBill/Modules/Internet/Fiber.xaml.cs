using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Dapper;
using TM.Helper;

namespace VinaphoneBill.Modules.Internet
{
    /// <summary>
    /// Interaction logic for Fiber.xaml
    /// </summary>
    public partial class Fiber : UserControl
    {
        string ext, fiber, ck, fiber_ct, fiber_th, hdwan, mega_ct, mega_th, dbin, phattrien, fiberpt;
        int month, year, daysInMonth;
        public Fiber()
        {
            InitializeComponent();
            btnProcessing.IsDefault = true;
            Loaded += UserControl_Loaded;

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtTime.Focus();
        }
        private void txtTime_KeyDown(object sender, KeyEventArgs e)
        {

        }
        //private void btnProcessing_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //Declare
        //        TM.OleDBF.DataSource = Common.Directories.data + txtTime.Text;
        //        var ext = ".dbf";
        //        var fiber = "fiber";
        //        var ck = "ck" + txtTime.Text;
        //        var fiber_ct = "fiber_ct" + txtTime.Text;
        //        var fiber_th = "fiber_th" + txtTime.Text;
        //        var hdwan = "hdwan" + txtTime.Text;
        //        var mega_ct = "mega_ct" + txtTime.Text;
        //        var mega_th = "mega_th" + txtTime.Text;

        //        //Create new file
        //        TM.IO.Delete(TM.OleDBF.DataSource + "/" + fiber + ext);
        //        TM.IO.Copy(TM.OleDBF.DataSource + "/" + fiber_th + ext, TM.OleDBF.DataSource + "/" + fiber + ext);
        //        TM.OleDBF.AddColumn(
        //            new List<string[]> {
        //                new[] { "ma_dvi", "n(2)" },
        //                new[] { "ma_cq", "c(20)" },//new[] { "ma_tt_hni", "n(2)" },
        //                new[] { "ma_st", "c(20)" },
        //                new[] { "ma_dt", "n(2)" },
        //                new[] { "ma_cbt", "n(8)" },
        //                new[] { "ma_tuyen", "c(50)" },
        //                new[] { "ma_in", "n(2)" },
        //                new[] { "tru_km", "n(10)" },
        //                new[] { "kieu", "c(15)" },
        //                new[] { "tbloaikb", "c(10)" },
        //                new[] { "dat_moi_tt", "n(2)" },
        //            },
        //            fiber, "fiber.dbf Add Column");

        //        TM.OleDBF.Execute(@"UPDATE a SET a.ma_dvi=b.ma_dvi,a.ma_cq=b.ma_cq,a.ma_st=b.ma_st,a.ma_dt=b.ma_dt,a.ma_cbt=b.ma_cbt,
        //                            a.ma_tuyen=b.ma_tuyen,a.ma_in=b.in_hd,a.tru_km=b.tru_km1,a.kieu=b.kieu,a.tbloaikb=b.tbloai,a.dat_moi_tt=b.dat_moi_tt 
        //                            FROM fiber AS a INNER JOIN hdwan1216 AS b ON a.calling=b.account WHERE a.calling is NOT null", "Update Fiber");
        //        TM.OleDBF.ExecuteOption("SET NULL OFF", @"INSERT INTO fiber(ma_dvi,calling,tbten,diachi,tel,ma_st,kieu,tbloaikb,ma_dt,ma_cq,ma_cbt,ma_in,
        //                            tru_km,ngay_sd,ma_tuyen,dat_moi_tt) SELECT ma_dvi,account as calling,ten_tb as tbten,dia_chi as diachi,
        //                            so_tb as tel,ma_st,kieu,tbloai as tbloaikb,ma_dt,ma_cq,ma_cbt,in_hd as ma_in,tru_km1 as tru_km,ngay_dk as ngay_sd,ma_tuyen,dat_moi_tt 
        //                            FROM hdwan1216 WHERE account NOT in (SELECT calling FROM fiber)", "Insert Fiber");
        //        //Delete BAK
        //        TM.IO.DeleteExt(TM.OleDBF.DataSource, new[] { ".BAK" });
        //        MessageBox.Show(Common.Language.Get("msgSucsess"));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        private void btnProcessing_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get last date DBF
                //SELECT  GOMONTH(DATE(), 1) - DAY(GOMONTH(DATE(), 1)) FROM fiber
                //Declare
                TM.OleDBF.DataSource = Common.Directories.data + txtTime.Text;
                ext = ".dbf";
                month = int.Parse(txtTime.Text[0].ToString() + txtTime.Text[1].ToString());
                year = int.Parse("20" + txtTime.Text[2].ToString() + txtTime.Text[3].ToString());
                daysInMonth = DateTime.DaysInMonth(year, month);
                var datetime = new DateTime(year, month, 1);
                fiber = "fiber";
                fiberpt = "fiberpt";
                ck = "ck" + txtTime.Text;
                fiber_ct = "fiber_ct" + txtTime.Text;
                fiber_th = "fiber_th" + txtTime.Text;
                hdwan = "hdwan" + txtTime.Text;
                mega_ct = "mega_ct" + txtTime.Text;
                mega_th = "mega_th" + txtTime.Text;
                dbin = "dbin" + txtTime.Text;
                phattrien = "phattrien" + txtTime.Text;

                //Tạo thêm cột thông tin cho fiber
                //tong->tien,tru_km1->giam_tru
                var listColumn = new List<string[]>() {
                    new[] { "ma_cq", "c(12)" },
                    new[] { "ma_tt_hni", "c(12)" },
                    new[] { "ma_st", "c(20)" },
                    new[] { "kieu", "c(15)" },
                    new[] { "tbloai1", "c(10)" },
                    new[] { "ma_dt", "n(2)" },
                    new[] { "ma_kh", "c(15)" },
                    new[] { "ma_cbt", "n(6)" },
                    new[] { "in_hd", "n(1)" },
                    new[] { "tinh_dt", "n(1)" },
                    new[] { "tb_khoan1", "n(1)" },
                    new[] { "ngay_dk", "d(8)" },
                    new[] { "ma_tuyen", "c(25)" },
                    new[] { "dat_moi_tt", "n(2)" },
                    new[] { "so_ngay_sd", "n(2)" },
                    new[] { "per_fee", "n(12,2)" },
                    new[] { "tt_thang", "n(2)" },
                };
                TM.OleDBF.AddColumn(listColumn, fiber, "AddColumn " + fiber);

                //Đổi tên cột ms_thue->ma_st trong file dbin
                listColumn = new List<string[]>()
                {
                    new[] { "ms_thue", "ma_st" },
                };
                TM.OleDBF.RenameColumn(listColumn, dbin, "Rename " + dbin);
                //Cập nhật thông tin cho fiber từ dbin
                TM.OleDBF.Execute(@"UPDATE a SET a.ma_cq=b.ma_kh,a.ma_tt_hni=b.ma_tt_hni,a.ma_st=b.ma_st,a.ma_dt=b.ma_dt,a.ma_kh=b.ma_kh,a.ma_cbt=b.ma_cbt,
                                    a.in_hd=1,a.tinh_dt=1,a.tb_khoan1=2,a.ngay_dk=b.ngay_dk,a.ma_tuyen=b.ma_tuyen,a.dat_moi_tt=b.dat_moi_tt 
                                    FROM " + fiber + " AS a INNER JOIN " + dbin + " AS b ON a.calling=b.account", "Update extra info " + fiber + " from " + dbin);

                //Cập nhật số ngày sử dụng và số ngày trong tháng
                //TM.OleDBF.Execute("UPDATE " + fiberpt + " SET songaysd=day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_sd),tongngay=day(ctod(\"" + datetime.ToShortDateString() + "\")-1)", "Update songaysd,tongngay " + fiberpt);
                //Phân loại trạng thái thuê bao
                //public const int _binh_thuong = 0;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._binh_thuong);
                UpdateTTThang("");
                UpdateSoNgaySD(daysInMonth.ToString());
                //public const int _su_dung = 1;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._su_dung + " WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month);
                UpdateTTThang("WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month, tt_thang._su_dung);
                UpdateSoNgaySD("day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_sd)", tt_thang._su_dung);

                //public const int _khoa = 2;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._khoa + " WHERE YEAR(ngay_khoa)=" + year + " AND MONTH(ngay_khoa)=" + month);
                UpdateTTThang("WHERE YEAR(ngay_khoa)=" + year + " AND MONTH(ngay_khoa)=" + month, tt_thang._khoa);
                UpdateSoNgaySD("day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_sd)", tt_thang._khoa);
                //public const int _mo = 3;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._mo + " WHERE YEAR(ngay_mo)=" + year + " AND MONTH(ngay_mo)=" + month);
                UpdateTTThang("WHERE YEAR(ngay_mo)=" + year + " AND MONTH(ngay_mo)=" + month, tt_thang._mo);
                UpdateSoNgaySD("day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_mo)", tt_thang._mo);
                //public const int _ket_thuc = 4;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._ket_thuc + " WHERE YEAR(ngay_kt)=" + year + " AND MONTH(ngay_kt)=" + month);
                UpdateTTThang("WHERE YEAR(ngay_kt)=" + year + " AND MONTH(ngay_kt)=" + month, tt_thang._ket_thuc);
                UpdateSoNgaySD("day(ngay_kt)", tt_thang._ket_thuc);
                //public const int _su_dung_ket_thuc = 5;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._su_dung_ket_thuc + " WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month + " AND (ngay_sd-ngay_kt)<0");
                UpdateTTThang("WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month + " AND (ngay_sd-ngay_kt)<0", tt_thang._su_dung_ket_thuc);
                UpdateSoNgaySD("ngay_kt-ngay_sd", tt_thang._su_dung_ket_thuc);
                //public const int _su_dung_khoa = 6;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._su_dung_khoa + " WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month + " AND (ngay_sd-ngay_khoa)<0");
                UpdateTTThang("WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month + " AND (ngay_sd-ngay_khoa)<0", tt_thang._su_dung_khoa);
                UpdateSoNgaySD("ngay_khoa-ngay_sd", tt_thang._su_dung_khoa);
                //public const int _khoa_mo = 7;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._khoa_mo + " WHERE YEAR(ngay_khoa)=" + year + " AND MONTH(ngay_khoa)=" + month + " AND (ngay_khoa-ngay_mo)<0");
                UpdateTTThang("WHERE YEAR(ngay_khoa)=" + year + " AND MONTH(ngay_khoa)=" + month + " AND (ngay_khoa-ngay_mo)<0", tt_thang._khoa_mo);
                UpdateSoNgaySD("day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_mo)", tt_thang._khoa_mo);
                //public const int _mo_khoa = 8;
                //TM.OleDBF.Execute("UPDATE " + fiber + " SET tt_thang=" + tt_thang._mo_khoa + " WHERE YEAR(ngay_mo)=" + year + " AND MONTH(ngay_mo)=" + month + " AND (ngay_mo-ngay_khoa)<0");
                UpdateTTThang("WHERE YEAR(ngay_mo)=" + year + " AND MONTH(ngay_mo)=" + month + " AND (ngay_mo-ngay_khoa)<0", tt_thang._mo_khoa);
                UpdateSoNgaySD("ngay_khoa-ngay_mo", tt_thang._mo_khoa);

                //
                //Set Kiểu fiber
                TM.OleDBF.Execute("UPDATE a SET kieu=(select type FROM pack WHERE profile=a.toc_do) FROM " + fiber + " as a", "Kiểu " + fiber);
                //Đơn giá theo ngày
                TM.OleDBF.Execute("UPDATE a SET per_fee=((select fee FROM pack WHERE profile=a.toc_do)/" + daysInMonth + ") FROM " + fiber + " as a", "Kiểu " + fiber);
                //Tính tiền
                TM.OleDBF.Execute("UPDATE a SET tien=ROUND(per_fee*so_ngay_sd,0) WHERE tt_thang<>0 FROM " + fiber + " as a", "UPDATE Tiền tt_thang<>" + tt_thang._binh_thuong + " " + fiber);
                TM.OleDBF.Execute("UPDATE a SET tien=(select fee FROM pack WHERE profile=a.toc_do) WHERE tt_thang=0 FROM " + fiber + " as a", "UPDATE Tiền tt_thang=" + tt_thang._binh_thuong + " " + fiber);
                //Tính VAT
                TM.OleDBF.Execute("UPDATE " + fiber + " SET vat=tien*0.1", "UPDATE VAT 10%" + fiber);


                //Tính tính hợp
                //var dt = TM.OleExcel.ToDataTable(TM.OleDBF.DataSource + "/BaoCao_THDV.xls", "SELECT * FROM BaoCao_THDV");
                //var excel = new TM.Interop.Excel(TM.OleDBF.DataSource+ "/thdv.xls");
                //var x = new System.IO.FileInfo(TM.OleDBF.DataSource + "/thdv.xls");
                //TM.Interop.ExcelStatic.DataSource = x.FullName;
                //var a = TM.Interop.ExcelStatic.ToDataTable();

                var prewMonth = datetime.AddMonths(-1);
                Guid fiberID = Guid.Parse("4e501432-f415-4449-af72-33ed55b6b0d9");
                var db = new ModelsTHDV.THDVContext();
                var thdv = (from c in db.customers
                            join cs in db.customer_services on c.id equals cs.customer_id
                            join cp in db.customer_packet on c.id equals cp.customer_id
                            join s in db.services on cs.services_id equals s.id
                            join p in db.packets on cp.packet_id equals p.id
                            where c.created_at.Value.Year == prewMonth.Year && c.created_at.Value.Month == prewMonth.Month && s.id == fiberID && c.flag > 0
                            select new { c, cs, cp, s, p }).ToList();
                var calling = new List<string>();
                var callingNot = new List<string>();
                var falses = new List<string>();
                foreach (var item in thdv)
                {
                    if (calling.Contains(item.cs.app_key.Trim()))
                        continue; //falses.Add(item.cs.app_key.Trim());
                    if (callingNot.Contains(item.cs.app_key.Trim()))
                        continue; //falses.Add(item.cs.app_key.Trim());
                    var tmp = TM.OleDBF.Execute("UPDATE " + fiber + " SET kieu=TRIM(kieu)+'+" + item.p.name + "' WHERE calling='" + item.cs.app_key + "'");
                    if (tmp > 0)
                        calling.Add(item.cs.app_key.Trim());
                    else
                        callingNot.Add(item.cs.app_key.Trim());
                }
                //var sql = "SELECT * FROM " + fiber;
                //var x = TM.OleDBF.Connection().Query(sql).ToList();
                //var a = x.Where(d=> !calling.Contains(((string)d.calling).Trim())).ToList();
                var xx = 0;
                ////Tạo bảng fiberpt 
                //TM.IO.Copy(TM.OleDBF.DataSource + "/" + fiber + ext, TM.OleDBF.DataSource + "/" + fiberpt + ext);
                //TM.OleDBF.Execute("DELETE FROM " + fiberpt, "DELETE " + fiberpt);
                //TM.OleDBF.Execute("PACK " + fiberpt, "PACK " + fiberpt);
                //TM.OleDBF.Execute("INSERT INTO " + fiberpt + " SELECT * FROM fiber WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month, "INSERT fiberpt");
                ////Thêm số ngày sử dụng vào fiberpt
                //listColumn = new List<string[]>()
                //{
                //    new[] { "songaysd", "n(2)" },
                //    new[] { "tongngay", "n(2)" },
                //};
                //TM.OleDBF.AddColumn(listColumn, fiberpt, "AddColumn " + fiberpt);
                //var datetime = new DateTime(year, month, 1);
                //TM.OleDBF.Execute("UPDATE " + fiberpt + " SET songaysd=day(ctod(\"" + datetime.ToShortDateString() + "\")-1)-day(ngay_sd),tongngay=day(ctod(\"" + datetime.ToShortDateString() + "\")-1)", "Update songaysd,tongngay " + fiberpt);

                ////
                //listColumn = new List<string[]>()
                //{
                //    new[] { "ms_thue", "ma_st" },
                //};
                //TM.OleDBF.RenameColumn(listColumn, dbin, "Rename " + dbin);
                //TM.OleDBF.Execute(@"UPDATE a SET a.ma_cq=b.ma_kh,a.ma_tt_hni=b.ma_tt_hni,a.ma_st=b.ma_st,a.ma_dt=b.ma_dt,a.ma_kh=b.ma_kh,a.ma_cbt=b.ma_cbt,
                //                    a.in_hd=1,a.tinh_dt=1,a.tb_khoan1=2,a.ngay_dk=b.ngay_dk,a.ma_tuyen=b.ma_tuyen,a.dat_moi_tt=b.dat_moi_tt 
                //                    FROM " + fiberpt + " AS a INNER JOIN " + dbin + " AS b ON a.calling=b.account", fiberpt + " Join " + dbin);

                //listColumn = new List<string[]>()
                //{
                //    new[] { "songaysd", "n(2)" },
                //};
                //TM.OleDBF.AddColumn(listColumn, fiberpt, "AddColumn " + fiberpt);

                ////Set Kiểu fiberpt
                //TM.OleDBF.Execute("UPDATE " + fiberpt + " SET kieu=(select type FROM pack WHERE profile=fiberpt.toc_do)", "Kiểu " + fiberpt);
                //TM.OleDBF.Execute("UPDATE " + fiberpt + " SET tien=ROUND(((select fee FROM pack WHERE profile=fiberpt.toc_do)/tongngay)*songaysd,0)", "Tiền " + fiberpt);
                //TM.OleDBF.Execute("UPDATE " + fiberpt + " SET vat=tien*0.1", "VAT " + fiberpt);




                ////Có 6 cái nul không tìm thấy trong dbin0117
                ////SELECT * FROM fiberpt WHERE ma_cq is null

                //account có trên fiber mà không có trên hdwan0117
                //SELECT * FROM fiber WHERE calling NOT in (SELECT b.calling FROM hdwan0117 as a INNER JOIN fiber as b ON a.account=b.calling)

                //Các thuê bao có ngày kết thúc
                //SELECT ngay_kt FROM fiber WHERE ngay_kt<>{}
                //Các thuê bao kết thúc trong tháng
                //SELECT * FROM fiber WHERE YEAR(ngay_kt)=2017 AND MONTH(ngay_kt)=01
                //Các thuê bao khóa trong tháng
                //SELECT * FROM fiber WHERE YEAR(ngay_khoa)=2017 AND MONTH(ngay_khoa)=01
                //Các thuê bao tồn tại trong pt0117 mà ko tồn tại trong pt0117ok
                //SELECT * FROM pt0117 WHERE calling NOT in(SELECT account FROM pt0117ok)
                //So Sánh tiền của fiberpt và pt0117ok
                //SELECT a.calling,a.kieu,a.tien,a.vat,b.tong,b.vat FROM fiberpt a INNER join pt0117ok b ON a.calling=b.account WHERE a.kieu='FTTH12M'
                //Các thuê bao có ngày kết thúc nhỏ hơn ngày sử dụng
                //SELECT * FROM fiber WHERE YEAR(ngay_kt)=2017 AND MONTH(ngay_kt)=01 AND DAY(ngay_kt)<DAY(ngay_sd)








                //UPDATE a SET a.ma_cq=b.ma_kh,a.ma_tt_hni=b.ma_tt_hni,a.ma_st=b.ma_st,a.ma_dt=b.ma_dt,a.ma_kh=b.ma_kh,a.ma_cbt=b.ma_cbt,a.in_hd=1,a.tinh_dt=1,a.tb_khoan1=2,a.ngay_dk=b.ngay_dk,a.ma_tuyen=b.ma_tuyen,a.dat_moi_tt=b.dat_moi_tt FROM fiberpt AS a INNER JOIN dbin0117 AS b ON a.calling=b.account

                //
                //var dtFiber = TM.OleDBF.ToDataTable("SELECT * FROM " + fiber + " WHERE YEAR(ngay_sd)=" + year + " AND MONTH(ngay_sd)=" + month);
                ////var dtDbin = TM.OleDBF.ToDataTable("SELECT * FROM " + dbin + " WHERE YEAR(ngay_sd)=20" + year + " AND MONTH(ngay_sd)=" + month);
                ////dtFiber.Columns.Add("songaysd");
                //for (int i = 0; i < dtFiber.Rows.Count; i++)
                //{
                //    var insert = string.Format("INSERT INTO " + phattrien + " VALUES({0})",
                //        "ma_dvi,account,ten_tb,dia_chi,so_tb,ma_st,kieu,tbloai,cuoc_tb,cuoc_ip,cuoc_sd,cuoc_ck,ma_dt,ma_cq," +
                //        "ma_kh,ma_cbt,in_hd,tinh_dt,tb_khoan,tong,vat,tru_km1,ngay_dk,ma_tt_hni,ma_tuyen,dat_moi_tt"
                //        );
                //    //TM.OleDBF.Execute(insert);
                //    var songaysd = (TM.Helper.TMDateTime.LastDate(2017, 1) - (DateTime)dtFiber.Rows[i]["ngay_sd"]).Days;
                //}

                //Delete BAK
                TM.IO.DeleteExt(TM.OleDBF.DataSource, new[] { ".BAK" });
                MessageBox.Show(Common.Language.Get("msgSucsess"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.subWindow.Close();
            MainWindow.Window.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Declare
            TM.OleDBF.DataSource = Common.Directories.data + txtTime.Text;
            ext = ".dbf";
            month = int.Parse(txtTime.Text[0].ToString() + txtTime.Text[1].ToString());
            year = int.Parse(txtTime.Text[2].ToString() + txtTime.Text[3].ToString());
            fiber = "fiber";
            ck = "ck" + txtTime.Text;
            fiber_ct = "fiber_ct" + txtTime.Text;
            fiber_th = "fiber_th" + txtTime.Text;
            hdwan = "hdwan" + txtTime.Text;
            mega_ct = "mega_ct" + txtTime.Text;
            mega_th = "mega_th" + txtTime.Text;
            dbin = "dbin" + txtTime.Text;

            var dt = TM.OleDBF.ToDataTable("SELECT distinct (SELECT MAX(id) FROM " + dbin + " WHERE account=o.account) as id FROM " + dbin + " o INNER JOIN (SELECT account, COUNT(*) AS dupeCount FROM " + dbin + " GROUP BY account HAVING COUNT(*) > 1) oc ON o.account=oc.account");
            for (int i = 0; i < dt.Rows.Count; i++)
                TM.OleDBF.Execute("DELETE FROM " + dbin + " WHERE id=" + dt.Rows[i][0]);
            MessageBox.Show("ok");
        }
        private void UpdateTTThang(string where, int tt = 0)
        {
            TM.OleDBF.Execute(
                string.Format("UPDATE {0} SET tt_thang={1} {2}", fiber, tt, where),
                string.Format("Update tt_thang: {0} - {1}", tt, fiber));
        }
        private void UpdateSoNgaySD(string so_ngay_sd, int tt = 0)
        {
            TM.OleDBF.Execute(
                string.Format("UPDATE {0} SET so_ngay_sd={1} WHERE tt_thang={2}", fiber, so_ngay_sd, tt),
                string.Format("Update so_ngay_sd: {0} - {1}", tt, fiber));
        }
    }
    public class tt_thang
    {
        public const int _binh_thuong = 0;
        public const int _su_dung = 1;
        public const int _khoa = 2;
        public const int _mo = 3;
        public const int _ket_thuc = 4;
        public const int _su_dung_ket_thuc = 5;
        public const int _su_dung_khoa = 6;
        public const int _khoa_mo = 7;
        public const int _mo_khoa = 8;
    }
}
