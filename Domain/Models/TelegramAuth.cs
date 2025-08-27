using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class TelegramAuth
{
    [Required(ErrorMessage = "This filed is required")]
    public string? Code { get; set; }
}