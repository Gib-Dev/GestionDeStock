using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GESTION_DES_STOCKS.Services
{
    public interface IValidationService
    {
        ValidationResult ValidateProduct(Product product);
        ValidationResult ValidateCategory(Category category);
        ValidationResult ValidateUser(User user);
        ValidationResult ValidatePassword(string password);
        ValidationResult ValidateEmail(string email);
        ValidationResult ValidateSku(string sku);
        ValidationResult ValidateBarcode(string barcode);
        bool IsValidPrice(decimal price);
        bool IsValidQuantity(int quantity);
    }

    public class ValidationService : IValidationService
    {
        public ValidationResult ValidateProduct(Product product)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(product);

            if (!Validator.TryValidateObject(product, context, results, true))
            {
                return new ValidationResult(string.Join("; ", results.Select(r => r.ErrorMessage)));
            }

            // Validations personnalisées
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return new ValidationResult("Le nom du produit est requis.");
            }

            if (product.ProductName.Length > 100)
            {
                return new ValidationResult("Le nom du produit ne peut pas dépasser 100 caractères.");
            }

            if (product.Price < 0)
            {
                return new ValidationResult("Le prix ne peut pas être négatif.");
            }

            if (product.StockQuantity < 0)
            {
                return new ValidationResult("La quantité en stock ne peut pas être négative.");
            }

            if (product.MinimumStockLevel < 0)
            {
                return new ValidationResult("Le niveau de stock minimum ne peut pas être négatif.");
            }

            if (!string.IsNullOrEmpty(product.SKU))
            {
                var skuValidation = ValidateSku(product.SKU);
                if (!skuValidation.IsValid)
                {
                    return skuValidation;
                }
            }

            if (!string.IsNullOrEmpty(product.Barcode))
            {
                var barcodeValidation = ValidateBarcode(product.Barcode);
                if (!barcodeValidation.IsValid)
                {
                    return barcodeValidation;
                }
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidateCategory(Category category)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(category);

            if (!Validator.TryValidateObject(category, context, results, true))
            {
                return new ValidationResult(string.Join("; ", results.Select(r => r.ErrorMessage)));
            }

            // Validations personnalisées
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return new ValidationResult("Le nom de la catégorie est requis.");
            }

            if (category.CategoryName.Length > 100)
            {
                return new ValidationResult("Le nom de la catégorie ne peut pas dépasser 100 caractères.");
            }

            if (!string.IsNullOrEmpty(category.Color) && !IsValidColor(category.Color))
            {
                return new ValidationResult("Le format de couleur n'est pas valide (format hexadécimal requis).");
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidateUser(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                return new ValidationResult(string.Join("; ", results.Select(r => r.ErrorMessage)));
            }

            // Validations personnalisées
            if (string.IsNullOrWhiteSpace(user.Username))
            {
                return new ValidationResult("Le nom d'utilisateur est requis.");
            }

            if (user.Username.Length < 3 || user.Username.Length > 50)
            {
                return new ValidationResult("Le nom d'utilisateur doit contenir entre 3 et 50 caractères.");
            }

            if (!IsValidUsername(user.Username))
            {
                return new ValidationResult("Le nom d'utilisateur ne peut contenir que des lettres, chiffres et tirets.");
            }

            var emailValidation = ValidateEmail(user.Email);
            if (!emailValidation.IsValid)
            {
                return emailValidation;
            }

            if (!string.IsNullOrEmpty(user.FirstName) && user.FirstName.Length > 50)
            {
                return new ValidationResult("Le prénom ne peut pas dépasser 50 caractères.");
            }

            if (!string.IsNullOrEmpty(user.LastName) && user.LastName.Length > 50)
            {
                return new ValidationResult("Le nom ne peut pas dépasser 50 caractères.");
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("Le mot de passe est requis.");
            }

            if (password.Length < 8)
            {
                return new ValidationResult("Le mot de passe doit contenir au moins 8 caractères.");
            }

            if (password.Length > 100)
            {
                return new ValidationResult("Le mot de passe ne peut pas dépasser 100 caractères.");
            }

            // Vérifier la complexité
            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            if (!hasUpperCase || !hasLowerCase || !hasDigit || !hasSpecialChar)
            {
                return new ValidationResult("Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un caractère spécial.");
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("L'adresse email est requise.");
            }

            if (email.Length > 100)
            {
                return new ValidationResult("L'adresse email ne peut pas dépasser 100 caractères.");
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    return new ValidationResult("L'adresse email n'est pas valide.");
                }
            }
            catch
            {
                return new ValidationResult("L'adresse email n'est pas valide.");
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidateSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                return new ValidationResult("Le SKU est requis.");
            }

            if (sku.Length > 50)
            {
                return new ValidationResult("Le SKU ne peut pas dépasser 50 caractères.");
            }

            // Le SKU doit contenir seulement des lettres, chiffres et tirets
            if (!Regex.IsMatch(sku, @"^[A-Za-z0-9\-_]+$"))
            {
                return new ValidationResult("Le SKU ne peut contenir que des lettres, chiffres, tirets et underscores.");
            }

            return ValidationResult.Success!;
        }

        public ValidationResult ValidateBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return new ValidationResult("Le code-barres est requis.");
            }

            if (barcode.Length > 50)
            {
                return new ValidationResult("Le code-barres ne peut pas dépasser 50 caractères.");
            }

            // Le code-barres doit contenir seulement des chiffres
            if (!Regex.IsMatch(barcode, @"^[0-9]+$"))
            {
                return new ValidationResult("Le code-barres ne peut contenir que des chiffres.");
            }

            // Vérifier la longueur standard (EAN-13 = 13 chiffres)
            if (barcode.Length != 13 && barcode.Length != 8 && barcode.Length != 12)
            {
                return new ValidationResult("Le code-barres doit contenir 8, 12 ou 13 chiffres.");
            }

            return ValidationResult.Success!;
        }

        public bool IsValidPrice(decimal price)
        {
            return price >= 0 && price <= 999999.99m;
        }

        public bool IsValidQuantity(int quantity)
        {
            return quantity >= 0 && quantity <= 999999;
        }

        private bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, @"^[A-Za-z0-9\-_]+$");
        }

        private bool IsValidColor(string color)
        {
            return Regex.IsMatch(color, @"^#[0-9A-Fa-f]{6}$");
        }
    }
} 