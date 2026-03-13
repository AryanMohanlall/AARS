using System.ComponentModel.DataAnnotations;

namespace AARS.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}