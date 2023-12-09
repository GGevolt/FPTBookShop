using FPTBookShopWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShopWeb.Data
{
	public class ApplicationDBContext : IdentityDbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookCategory> BookCategories { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option):base(option)
		{ 

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<BookCategory>()
				.HasKey(bc => new { bc.BookId, bc.CategoryId });

			modelBuilder.Entity<BookCategory>()
				.HasOne(bc => bc.Book)
				.WithMany(b => b.BookCategories)
				.HasForeignKey(bc => bc.BookId);

			modelBuilder.Entity<BookCategory>()
				.HasOne(bc => bc.Category)
				.WithMany(c => c.BookCategories)
				.HasForeignKey(bc => bc.CategoryId);
            modelBuilder.Entity<Category>().HasData(
				new Category { ID = 1, Name = "Romansss", Description = "A lot of roman stories" },
				new Category { ID = 2, Name = "Action", Description = "Show you how is an action"},
				new Category { ID = 3, Name = "Horror", Description = "So scary"},
				new Category { ID = 4, Name = "Science", Description = "For anyone who loves science" },
				new Category { ID = 5, Name = "History", Description = "You can know alot about the world" }
				);
		}
	}
}
