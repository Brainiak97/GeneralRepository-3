using System.ComponentModel.DataAnnotations;

namespace PolyclinicService.Domain.Models;

/// <summary>
/// Специализации врачей.
/// </summary>
public enum SpecializationType : short
{
    /// <summary>
    /// Терапевт.
    /// </summary>
    [Display(Name = "Терапевт")]
    Internist = 1,
    
    /// <summary>
    /// Хирург.
    /// </summary>
    [Display(Name = "Хирург")]
    Surgeon = 2,
    
    /// <summary>
    /// Дерматолог.
    /// </summary>
    [Display(Name = "Дерматолог")]
    Dermatologists = 3,
    
    /// <summary>
    /// Кардиолог.
    /// </summary>
    [Display(Name = "Кардиолог")]
    Cardiologists = 4,
    
    /// <summary>
    /// Эндокринолог.
    /// </summary>
    [Display(Name = "Эндокринолог")]
    Endocrinologists = 5,
    
    /// <summary>
    /// Гастроэнтеролог.
    /// </summary>
    [Display(Name = "Гастроэнтеролог")]
    Gastroenterologists = 6,
    
    /// <summary>
    /// Нефролог.
    /// </summary>
    [Display(Name = "Нефролог")]
    Nephrologists = 6,
    
    /// <summary>
    /// Невролог.
    /// </summary>
    [Display(Name = "Невролог")]
    Neurologists = 7,
    
    /// <summary>
    /// Аллерголог.
    /// </summary>
    [Display(Name = "Аллерголог")]
    Allergists = 8,
    
    /// <summary>
    /// Гинеколог.
    /// </summary>
    [Display(Name = "Гинеколог")]
    Gynecologists = 9,
    
    /// <summary>
    /// Уролог.
    /// </summary>
    [Display(Name = "Уролог")]
    Urologists = 10,
    
    /// <summary>
    /// Онколог.
    /// </summary>
    [Display(Name = "Онколог")]
    Oncologists = 11,
    
    /// <summary>
    /// Офтальмолог.
    /// </summary>
    [Display(Name = "Офтальмолог")]
    Ophthalmologists = 12,
    
    /// <summary>
    /// Отоларинголог.
    /// </summary>
    [Display(Name = "Отоларинголог")]
    Otolaryngologist = 13,
}