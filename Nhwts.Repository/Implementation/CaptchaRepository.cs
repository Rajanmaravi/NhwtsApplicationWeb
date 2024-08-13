using Nhwts.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nhwts.Repository.Implementation
{
    public class CaptchaRepository : ICaptchaRepository
    {
        private readonly Random _random = new Random();

        public async Task<string> GenerateCaptchaImageAsync()
        {
            var captchaText = GenerateRandomText(5); // Generate a random CAPTCHA text
            var image = new Bitmap(120, 31);
            var graphics = Graphics.FromImage(image);
            var font = new Font("Arial", 20, FontStyle.Bold);
            var brush = new SolidBrush(Color.Black);

            graphics.Clear(Color.White);
            graphics.DrawString(captchaText, font, brush, 10, 5);

            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                var base64String = Convert.ToBase64String(memoryStream.ToArray());
                return base64String;
            }
        }

        

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
