using System;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options)
    : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

}
