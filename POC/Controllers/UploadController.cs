using Microsoft.AspNetCore.Mvc;
using POC.Models;
using System.Text;

namespace POC.Controllers
{
    [Route("Upload")]
    public class UploadController : Controller
    {
        public IActionResult Upload()
        {
            if (TempData["Username"] != null && TempData["password"] != null)
            {
                var output = new List<UploadModel>();
                return View(output);
            }
            else
                return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);

                if (ext == ".txt")
                {
                    StringBuilder result = new StringBuilder();
                    var output = new List<UploadModel>();
                    using (StreamReader sr = new StreamReader(file.OpenReadStream()))
                    {
                        while (sr.Peek() >= 0)
                            result.AppendLine(sr.ReadLine().ToString());

                        var words = result.ToString()
                                        .Replace(',', ' ')
                                        .Replace('#', ' ')
                                        .Replace('@', ' ')
                                        .Replace('$', ' ')
                                        .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)


                                        .ToList();

                        var wordsCount = new Dictionary<string, int>();

                        foreach(var word in words)
                        {
                            if (wordsCount.ContainsKey(word))
                                wordsCount[word]++;
                            else
                                wordsCount.Add(word,1);
                        }

                        foreach (var word in wordsCount)
                        {
                            output.Add(new UploadModel(word.Key, word.Value.ToString()));
                        }
                        //if(words.Count>0)
                        //    var viewwordsCount = 
                    }
                    return View(output);

                }
                else
                {
                    TempData["msg"] = "<script>alert('Please Select Only TextFiles');</script>";

                    return View(this);

                }
            }
            TempData["msg"] = "<script>alert('Please Select some file');</script>";
            return View();
        }
    }
}

    