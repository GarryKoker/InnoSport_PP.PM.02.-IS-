using Microsoft.EntityFrameworkCore;
using InnoSport.Data;
using InnoSport.Models;
using System;
using System.Linq;
using Xunit;
using Section = InnoSport.Models.Section;

public class ProjectCoreTests
{
    private AppDBContext GetInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDBContext(options);
    }

    [Fact]
    public void CanCreateUser()
    {
        using var db = GetInMemoryDb();
        var user = new User
        {
            Name = "Иван",
            Surname = "Иванов",
            PhoneNumber = "1234567890",
            Password = "pass",
            Role = 1
        };
        db.Users.Add(user);
        db.SaveChanges();

        Assert.Single(db.Users);
        Assert.Equal("Иван", db.Users.First().Name);
    }

    [Fact]
    public void CanCreateSection()
    {
        using var db = GetInMemoryDb();
        var section = new Section
        {
            Name = "Футбол",
            Type = "Спорт",
            Description = "Секция футбола"
        };
        db.Sections.Add(section);
        db.SaveChanges();

        Assert.Single(db.Sections);
        Assert.Equal("Футбол", db.Sections.First().Name);
    }
}