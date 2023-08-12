using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EmailVerificationConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            
            string email = GetUserInput("Enter your email: ");




            string verificationCode = GenerateVerificationCode();

            
            SendVerificationEmail(email, verificationCode);
            Console.WriteLine("Verification email sent. Enter the verification code received:");
            string enteredCode = Console.ReadLine();

            if (enteredCode == verificationCode)
            {
                Console.WriteLine("User registered and verified!");
            }
            else
            {
                Console.WriteLine("Invalid verification code.");
            }
        }

        static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, pattern);
        }

        static string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        static void SendVerificationEmail(string recipientEmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("youremail@gmail.com");
                mail.To.Add(recipientEmail);
                mail.Subject = "Email Verification";
                mail.Body = "Your verification code is: " + code;
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("youremail@gmail.com", "yourpassword");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                Console.WriteLine("Verification email sent.");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending verification email: " + ex.Message);
            }
        }
    }
}
