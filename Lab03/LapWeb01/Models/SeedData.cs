using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LapWeb01.Data;
using System;
using System.Linq;

namespace LapWeb01.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LapWeb01Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LapWeb01Context>>()))
            {
                // Kiểm tra xem database đã có phim nào chưa
                if (context.Movie.Any())
                {
                    return;   // DB đã có dữ liệu thì không làm gì cả
                }

                // Nếu chưa có thì thêm 4 phim mẫu
                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }
                );
                context.SaveChanges(); // Lưu vào Database
            }
        }
    }
}