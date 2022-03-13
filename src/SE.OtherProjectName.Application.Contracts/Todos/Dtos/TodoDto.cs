﻿namespace SE.OtherProjectName.Application.Contracts.Todos.Dtos;

public class TodoDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public DateTime PublishDate { get; set; }

    public Decimal Price { get; set; }
}