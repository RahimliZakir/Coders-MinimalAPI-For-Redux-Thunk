using Coders.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coders.WebAPI.Models.DataContexts
{
    public static class DatabaseInitializer
    {
        async public static Task<IApplicationBuilder> Initialize(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateAsyncScope())
            {
                CoderDbContext db = scope.ServiceProvider.GetRequiredService<CoderDbContext>();

                if (!await db.Coders.AnyAsync())
                {
                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name= "Aylmar",
                        Job = "Javascript",
                        Email = "asimpson0@vistaprint.com"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Tallulah",
                        Job = "CSS",
                        Email = "tomullally1@go.com"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Lon",
                        Job = "Python",
                        Email = "ldankov2@uol.com.br"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Lynea",
                        Job = "CSS",
                        Email = "lpeakman3@nhs.uk"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Germayne",
                        Job = "Javascript",
                        Email = "john@gmail.com"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Parsifal",
                        Job = "Javascript",
                        Email = "pmarians5@pen.io"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Nathalie",
                        Job = "Javascript",
                        Email = "nloveless6@gmail.com"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Peri",
                        Job = "Python",
                        Email = "pofallone7@gmail.com"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Drud",
                        Job = "Javascript",
                        Email = "dtamplin8@jugem.jp"
                    });

                    await db.Coders.AddAsync(new Coder
                    {
                        First_Name = "Trudy",
                        Job = "Javascript",
                        Email = "tstobart9@gmail.com"
                    });

                    await db.SaveChangesAsync();
                }
            }

            return app;
        }
    }
}
