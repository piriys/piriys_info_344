using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewSparkProject.Models
{
    public class RegisterModel
    {
        public String FullName()
        {
            return FirstName + " " + LastName;
        }

        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        /// <summary>
        /// Username can contain only lowercase characters, numbers, underscore (_), and dash (-).
        /// REGEX from http://regexlib.com/
        /// </summary>
        [Display(Name = "Username")]
        [RegularExpression(@"^[a-z0-9_-]+$", ErrorMessage = "Username can contain only lowercase characters (a-z), numbers (0-9), dash (-), and underscore (_)")]
        [StringLength(12, ErrorMessage = "Username is too long, please limit your username to 12 characters")]
        [Required(ErrorMessage = "Please enter your username")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(21, ErrorMessage = "The password must be between 6 and 21 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(21, ErrorMessage = "The confirmation password must be between 6 and 21 characters long.", MinimumLength = 6)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// First name property can contain only alphabet, single quote ('), and dash (-).
        /// REGEX from http://regexlib.com/
        /// </summary>
        [Display(Name = "First Name")]
        [RegularExpression(@"^([ \u00c0-\u01ffa-zA-Z'])+$", ErrorMessage = "Please enter valid first name")]
        [StringLength(255, ErrorMessage = "First name is too long, please enter first 255 characters")]
        [Required(ErrorMessage = "Please enter your first name")]
        public String FirstName { get; set; }

        /// <summary>
        /// Last name property can contain only alphabet, single quote ('), and dash (-).
        /// REGEX from http://regexlib.com/
        /// </summary>
        [Display(Name = "Last Name")]
        [RegularExpression(@"^([ \u00c0-\u01ffa-zA-Z'])+$", ErrorMessage = "Please enter valid last name")]
        [StringLength(255, ErrorMessage = "Last name is too long, please enter first 255 characters")]
        [Required(ErrorMessage = "Please enter your last name")]
        public String LastName { get; set; }

        /// <summary>
        /// Gender property for the radio button in the form.
        /// </summary>
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Please select your gender")]
        public String Gender { get; set; }

        /// <summary>
        /// Date of Birth property only validates when the data input is in the form of month/day/year.
        /// </summary>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [Required(ErrorMessage = "Please enter your date of birth")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Email property only validates when the data input is in the form of example@domain.com.
        /// REGEX from http://regexlib.com/
        /// </summary>
        [Display(Name = "Account Email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please enter a valid email address in example@domain.com format")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email address")]
        public String Email { get; set; }

        public string Authorizer { get; set; }

        public string AuthorizationID { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime LastLoginDate { get; set; }
    }

    public class LogOnModel
    {
        /// <summary>
        /// Username can contain only lowercase characters, numbers, underscore (_), and dash (-).
        /// REGEX from http://regexlib.com/
        /// </summary>
        [Display(Name = "Username")]
        [RegularExpression(@"^[a-z0-9_-]+$", ErrorMessage = "Username can contain only lowercase characters (a-z), numbers (0-9), dash (-), and underscore (_)")]
        [StringLength(12, ErrorMessage = "Username is too long, please limit your username to 12 characters")]
        [Required(ErrorMessage = "Please enter your username")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
