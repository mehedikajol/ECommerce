﻿namespace ECommerce.Application.BusinessEntities;

public class UserProfile
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Gender { get; set; }
    public string ProfilePictureUrl { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public string InsertedBy { get; set; } = string.Empty;
}
