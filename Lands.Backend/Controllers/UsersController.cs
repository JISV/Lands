namespace Lands.Backend.Controllers
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Net;
    using System.Web.Mvc;
    using Backend.Models;
    using Domain;
    using Helpers;

    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //Cambie en la linea de abajo el tipo User por un UserView y lo llamo view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserView view)
        {
            if (ModelState.IsValid)
            {
                //como no recibo un user, convierto el objeto view en user
                //usando el siguiente metodo ToUser que lo defino para esto
                var user = this.ToUser(view);
                db.Users.Add(user);
                await db.SaveChangesAsync();
                UsersHelper.CreateUserASP(view.Email, "User", view.Password);//aqui tambien creo el usuario del asp.net
                return RedirectToAction("Index");                            //para que las dos tablas esten sincronizadas
            }

            return View(view);

        }

        //Aqui cambie el objeto generico de retorno por un objeto User
        private User ToUser(UserView view)
        {
            return new User
            {
                //mapeo los campos de los objetos para convertir
                //el userview en un user
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
                ImagePath= view.ImagePath,
                Telephone=view.Telephone,
                UserId=view.UserId

            };
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,FirstName,LastName,Email,Telephone,ImagePath")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
