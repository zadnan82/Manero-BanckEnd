﻿using System.ComponentModel.DataAnnotations;

namespace Manero_BanckEnd.Entities;

public class KeyEntity
{
    [Key]
    public int Id { get; set; } 
    public string UserId { get; set; } = null!;

    public UserEntity User { get; set; } = null!; 

    public string Key { get; set; } = null!; 


}
