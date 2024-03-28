using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;

namespace QuasarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TramController : ControllerBase
    {
        [HttpGet]
        [Route("gettram")]
        public IActionResult GetTram( int pageIndex, int pageSize)
        {
            try {
                using (var context = new MyDbContext())
                {
                    List<Tram> trams = new List<Tram>();
                    ResponsePaging response = new ResponsePaging();
                    if (pageIndex > 0 && pageSize > 0)
                    {
                        trams = context.Trams.OrderBy(t => t.MaTram)
                                                        .Skip((pageIndex - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToList();
                        response.trams = trams;
                        
                        int count  = context.Trams.Count();
                        response.total = (int)Math.Ceiling(count / (double)pageSize);

                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }
        [HttpPost]
        [Route("addtram")]
        public IActionResult AddTram(Tram tram)
        {
            using(var context = new MyDbContext())
            {
                context.Trams.Add(tram);
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpGet]
        [Route("gettrambymatram")]
        public Tram GetTramByMaTram([FromHeader] string maTram)
        {
            using(var context = new MyDbContext())
            {
                Tram tram = context.Trams.FirstOrDefault(t => t.MaTram.Equals(maTram));
                return tram;
            }
        }
        [HttpPost]
        [Route("updatetram")]
        public IActionResult UpdateTram([FromBody] Tram tram)
        {
            using(var context = new MyDbContext())
            {
                Tram tramToUpdate = context.Trams.FirstOrDefault(t => t.MaTram.Equals(tram.MaTram));
                if(tramToUpdate == null)
                {
                    return NotFound();
                }
                tramToUpdate.TenTram = tram.TenTram;
                tramToUpdate.DiaChi = tram.DiaChi;
                tramToUpdate.TenTinh = tram.TenTinh;
                tramToUpdate.KhuVuc = tram.KhuVuc;
                tramToUpdate.LoaiTram = tram.LoaiTram;
                tramToUpdate.KinhDo = tram.KinhDo;
                tramToUpdate.ViDo = tram.ViDo;
                tramToUpdate.GhiChu = tram.GhiChu;
                tramToUpdate.PhanCapTram = tram.PhanCapTram;
                context.SaveChanges();
                return Ok();
            }
        }
        [HttpPost]
        [Route("filteredTram")]
        public IActionResult Filter([FromBody] FilterRequest req) 
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    List<Tram> trams = new List<Tram>();
                    ResponsePaging response = new ResponsePaging();
                    if(req != null)
                    {
                        if (req.KhuVuc.Equals("Tất Cả") && req.LoaiTram.Equals("Tất Cả") && req.TenTinh.Equals("Tất Cả") && req.PhanCapTram.Equals("Tất Cả")) 
                        {                        
                            trams = context.Trams.Skip((req.pageIndex - 1) * req.pageSize)
                                                        .Take(req.pageSize)
                                                        .ToList();
                            response.trams = trams;
                            int count = context.Trams.Count();
                            response.total = (int)Math.Ceiling(count / (double)req.pageSize);
                            return Ok(response);
                        }
                        else 
                        {
                            trams = context.Trams.Where(tram => tram.KhuVuc.Equals(req.KhuVuc) && tram.PhanCapTram.Equals(req.PhanCapTram) && tram.LoaiTram.Equals(req.LoaiTram) && tram.TenTinh.Equals(req.TenTinh)).Skip((req.pageIndex - 1) * req.pageSize)
                                                        .Take(req.pageSize)
                                                        .ToList(); 
                            response.trams = trams;
                            int count = trams.Count();
                            response.total = (int)Math.Ceiling(count / (double)3);
                            return Ok(response);
                        }
                    }
                    return BadRequest();
                }
            }
            catch(Exception ex) 
            {
                return Ok(ex);
            }
        }
    }
}
