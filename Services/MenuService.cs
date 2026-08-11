using CampusEats.Api.Dtos;
using CampusEats.Api.Models;

namespace CampusEats.Api.Services;

public class MenuService : IMenuService
{
    private readonly List<MenuItem> _items;
    private int _nextId;

    public MenuService() // constructor — runs once, seeds initial data
    {
        _items = new()
        {
            new() { Id = 1, Name = "Kottu Roti", Price = 750m, Category = "Mains" },
            new() { Id = 2, Name = "Fried Rice", Price = 850m, Category = "Mains" },
            new() { Id = 3, Name = "Watalappan", Price = 350m, Category = "Dessert" },
        };
        _nextId = 4;
    }

    private static MenuItemDto ToDto(MenuItem m) =>
        new(m.Id, m.Name, m.Price, m.Category, m.Available);

    public IEnumerable<MenuItemDto> GetAll() =>
        _items.Select(ToDto);

    public MenuItemDto? GetById(int id)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);
        return m is null ? null : ToDto(m);
    }


       
    public MenuItemDto Create(CreateMenuItemDto dto)
    {
        var item = new MenuItem
        {
            Id = _nextId++,
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            Available = true
        };
        _items.Add(item);
        return ToDto(item);
    }

    public bool Update(int id, CreateMenuItemDto dto)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);
        if (m is null) return false; // guard clause
        m.Name = dto.Name;
        m.Price = dto.Price;
        m.Category = dto.Category;
        return true;
    }

    public bool Delete(int id)
    {
        var m = _items.FirstOrDefault(x => x.Id == id);
        if (m is null) return false;
        _items.Remove(m);
        return true;
    }


    
}

