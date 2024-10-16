using BookXtation.DAL.Models;
using BookXtation.DAL.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooXtation.BLL.Repositories.Specifications
{
    public static class BooXtationContextSeeding
    {
        public async static Task SeedAsync(BooXtationContext context)
        {
            if (!context.Authors.Any())
            {
                var authorData = File.ReadAllText("../BooXtation.BLL/Seeding/Author.json");
                var authors = JsonSerializer.Deserialize<List<Author>>(authorData);

                foreach (var item in authors)
                {
                    await context.Set<Author>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Categories.Any())
            {
                var categorydata = File.ReadAllText("../BooXtation.BLL/Seeding/Category.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categorydata);

                foreach (var item in categories)
                {
                    await context.Set<Category>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Publishers.Any())
            {
                var publisherdata = File.ReadAllText("../BooXtation.BLL/Seeding/Publisher.json");
                var publishers = JsonSerializer.Deserialize<List<Publisher>>(publisherdata);

                foreach (var item in publishers)
                {
                    await context.Set<Publisher>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Books.Any())
            {
                var bookdata = File.ReadAllText("../BooXtation.BLL/Seeding/Book.json");
                var books = JsonSerializer.Deserialize<List<Book>>(bookdata);

                foreach (var item in books)
                {
                    await context.Set<Book>().AddAsync(item);
                    await context.SaveChangesAsync();
                }
            }




        }
    }
}
