using Analytics_Hub.Models;
using Analytics_Hub.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Analytics_Hub.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _db;
        public DashboardController(DatabaseContext db)
        {
            _db = db;
        }
        public IActionResult Display()
        {
            IEnumerable<Category> objectCategoryList = _db.Categories.ToList();
            return View(objectCategoryList);
        }
        //public IActionResult RunPythonScript(int? id)
        //{
        //    IEnumerable<PythonScript> pythonScripts = _db.PythonScripts.Find(id);
        //    return View();
        //}

        public IActionResult RunPythonScript(int id)
        {
            var script = _db.PythonScripts
            .Where(obj => obj.CategoryId == id)
            .FirstOrDefault();

            // Set the path to your Streamlit executable
            string streamlitPath = @"C:\Users\Aman\AppData\Local\Programs\Python\Python310\Scripts\streamlit.exe";

            // Set the path to your Streamlit app file or directory
            string streamlitAppPath = script.ScriptName;
                //@"C:\Users\Aman\Desktop\Molecular_Descriptor_App\app.py";

            string appDirectory = Path.GetDirectoryName(streamlitAppPath);

            // Set the current working directory to the app directory
            Directory.SetCurrentDirectory(appDirectory);

            // Build the command to run Streamlit app
            string command = $"\"{streamlitPath}\" run \"{streamlitAppPath}\"";

            // Create a process to run the Streamlit app
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();

            // Pass the command to the command prompt
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            // Wait for the process to exit
            process.WaitForExit();

            // Get the output and error messages
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // Optionally, you can handle the output and error as needed

            // Close the process
            process.Close();

            return RedirectToAction("Display");
        }
    }
}