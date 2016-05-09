using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using KindCMS.Models;
using Microsoft.AspNet.Authorization;
using KindCMS.Areas.Admin.ViewModels.Key;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KindCMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class KeyController : Controller
    {
        private ApplicationDbContext _Db;
        private KeyManager KeyManager;

        public KeyController(ApplicationDbContext Db)
        {
            _Db = Db;
            this.KeyManager = new KeyManager(Db);
        }
        // 列表
        public IActionResult Index(string key,string q)
        {
            
            if (string.IsNullOrEmpty(key))
            {
                ViewBag.ParentKey = "/";
            }else
            {
                try
                {
                    var PC = _Db.Set<Classes>().FirstOrDefault(c => c.Key == key);
                    ViewBag.ParentKey = PC.PathIndexs?.Replace(",","/") + "/" + PC.Key + "/";
                }
                catch
                {
                    ViewBag.ParentKey = "##";
                }
            }
            

            var Keys = _Db.Set<Classes>().AsQueryable();
            Keys = from k in Keys orderby k.Order ascending select k;
            if (!string.IsNullOrEmpty(key))
            {
                Keys = from k in Keys where k.Parent == key select k;
            }else
            {
                Keys = from k in Keys where k.Parent == null||k.Parent =="" select k;
            }
            if (!string.IsNullOrEmpty(q))
            {
                Keys = Keys.Where(k => k.Key.Contains(q) || k.Name.Contains(q));
            }
            IQueryable<KeyListViewModel> p = (from k in Keys
                                              let ct = (from k2 in _Db.Set<Classes>() where k2.Parent == k.Key select k2).LongCount()
                                              select new KeyListViewModel()
                                              {
                                                   CLS =  new Classes()
                                                  {
                                                      Key = k.Key,
                                                      Name = k.Name,
                                                      Parent = k.Parent,
                                                      Order = k.Order,
                                                      PathIndexs = k.PathIndexs
                                                  },
                                                  xCount = ct
                                              });

            var list = p?.ToList();
            return View(list);
        }
        //添加 Get
        public IActionResult Create(string parent)
        {
            ViewBag.Parent = parent;
            return View(new Classes() { Order = 10 });
        }
        //添加 Post
        [HttpPost]
        public IActionResult Create(Classes c)
        {
            if (IsRepeat(c.Key))
            {
                ModelState.AddModelError(nameof(Classes.Key), "主键不能重复");
            }
            string Index = "";
            if (!string.IsNullOrEmpty(c.Parent))
            {
                var parentObject = _Db.Set<Classes>().FirstOrDefault(C => C.Key == c.Parent);
                Index = parentObject.Parent + "," + parentObject.Key;
            }
            c.PathIndexs = Index;
            if (ModelState.IsValid)
            {
                _Db.Set<Classes>().Add(c);
                _Db.SaveChanges();
                return RedirectToAction(nameof(Index),new { key = c.Parent });
            }
            return View();
        }
        /// <summary>
        /// 检测主键是否重复
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsRepeat(string key)
        {
            var c = _Db.Set<Classes>().Where(k => k.Key == key).LongCount();
            return c != 0;
        }
        // 修改
        public IActionResult Edit(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("键是必须");
            }
            var xClass = _Db.Set<Classes>().Where(c => c.Key == key).FirstOrDefault();
            if(xClass == null)
            {
                return new HttpNotFoundResult();
            }
           
            return View(xClass);
        }
        // 修改 post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Classes c)
        {
            if(c == null)
            {
                throw new ArgumentNullException("提交的数据是空的");
            }
            if (ModelState.IsValid)
            {
                _Db.Set<Classes>().Update(c);
                _Db.SaveChanges();
                return RedirectToAction(nameof(Index), new { Key = c.Parent });
            }
            return View();
        }
        public IActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("键是必须");
            }
            var xClass = _Db.Set<Classes>().Where(c => c.Key == key).FirstOrDefault();
            //var childern = _Db.Set<Classes>().Where(c => c.Parent == xClass.Key);
            //var newChildern = childern.ToList();
            //newChildern.ForEach(c => c.Parent = xClass.Parent);
            //_Db.Set<Classes>().UpdateRange(newChildern);
            //_Db.Set<Classes>().Remove(xClass);
            long childern = _Db.Set<Classes>().Where(c => c.Parent == xClass.Key).LongCount();
            if(childern != 0)
            {
                ModelState.AddModelError("", "该项目包含其他的项目，无法删除");
                ModelState.AddModelError("", "哎嗨，我还就是不告诉你了还~！@#￥%……&*￥*（）——+~!@#$%^&*()_+\"\'?<>`·");
            }
            if (ModelState.IsValid)
            {
                _Db.Set<Classes>().Remove(xClass);
                _Db.SaveChanges();
                return RedirectToAction(nameof(Index), new { key = xClass.Parent });
            }

            return new KindCMS.Utility.JsErrorResult();
        }
    }

    #region 实用程序
    public class KeyManager
    {
        private ApplicationDbContext _Db;

        public KeyManager(ApplicationDbContext Db):this(Db.Set<Classes>())
        {
            this._Db = Db;
        }

        public KeyManager(IQueryable<Classes> cls)
        {
            Source = cls;
        }

        public IQueryable<Classes> Source { get; private set; }

        public bool RebuildIndex()
        {
            byte[] a = new byte[3];
            return false;
        }

    }
    #endregion
}
