using Microsoft.WindowsAPICodePack.Dialogs;
using Syncfusion.DocIO.DLS;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace WPF_Starter.Abstract
{
    internal class ViewModelBase: DevExpress.Mvvm.ViewModelBase
    {
        #region Properties

        #region Version

        public string ApplicationName => "$safeprojectname$";

        public string Title => $"{ApplicationName} v0.3";

        #endregion

        #endregion

        #region Dialog Functions
        /// <summary>
        /// Display an error message
        /// </summary>
        /// <param name="ex">Your exception</param>
        /// <param name="message">Title text. Typically, put here your method name</param>
        public static void ErrorMessage(Exception ex)
        {
            try
            {
                StackTrace stackTrace = new StackTrace(ex);
                System.Reflection.MethodBase method = stackTrace.GetFrame(stackTrace.FrameCount - 1).GetMethod();
                string titleText = method.Name;

                MessageBox.Show(string.Format(ex.Message + "\n\n" + ex.StackTrace + "\n\n{0}", "Please screenshot and send procedures on how this error occured. Thank you."), titleText, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        /// <summary>
        /// Display an error message
        /// </summary>
        /// <param name="ex">Your exception</param>
        /// <param name="message">Title text. Typically, put here your method name</param>
        public static void ErrorMessage(Exception ex, string title)
        {
            try
            {
                StackTrace stackTrace = new StackTrace(ex);
                System.Reflection.MethodBase method = stackTrace.GetFrame(stackTrace.FrameCount - 1).GetMethod();
                string titleText = title;

                MessageBox.Show(string.Format(ex.Message + "\n\n" + ex.StackTrace + "\n\n{0}", "Please screenshot and send procedures on how this error occured. Thank you."), titleText, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2, title);
            }
        }

        /// <summary>
        /// Displays an information messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void InformationMessage(string message, string title)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        /// <summary>
        /// Displays a warning messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void WarningMessage(string message, string title)
        {
            try
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }
        /// <summary>
        /// Displays a warning messagebox
        /// </summary>
        /// <param name="message">Text</param>
        /// <param name="title">Title text</param>
        public static void WarningMessage(string message)
        {
            try
            {
                WarningMessage(message, "Warning");
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }
        }

        /// <summary>
        /// Displays a yes/no/cancel messagebox. Returns a MessageBoxResult. Either true or false.
        /// </summary>
        /// <param name="message">Text to ask</param>
        /// <returns>MessageBoxResult result. Either true or false.</returns>
        public static MessageBoxResult YesNoCancelDialog(string message)
        {
            MessageBoxResult res = MessageBoxResult.None;

            try
            {
                res = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);
            }
            catch (Exception ex2)
            {
                ErrorMessage(ex2);
            }

            return res;
        }
        #endregion

        #region Shared functions

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Open the file chooser and return the selected files
        /// </summary>
        /// <returns></returns>
        public List<string> GetFilePaths(string DisplayName, string ExtensionList, string Title)
        {
            List<string> pathList = null;

            try
            {
                Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
                {
                    Title = Title,
                    Multiselect = true
                };

                dialog.Filters.Add(new Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter(DisplayName, ExtensionList));

                if (dialog.ShowDialog() != Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
                {
                    return null;
                }

                pathList = dialog.FileNames.ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

            return pathList;
        }
        /// <summary>
        /// Open the file chooser and return the selected file or folder
        /// </summary>
        /// <returns></returns>
        public string GetFilePath(string DisplayName, string ExtensionList, string Title)
        {
            string path = null;

            try
            {
                Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog dialog = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
                {
                    Title = Title,
                };

                dialog.Filters.Add(new Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter(DisplayName, ExtensionList));

                if (dialog.ShowDialog() != Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
                {
                    return null;
                }

                path = dialog.FileName;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

            return path;
        }

        /// <summary>
        /// Open the folder chooser and return the selected paths
        /// </summary>
        /// <param name="IsMultiSelect">Default is false. True = Select 2 or more files</param>
        /// <returns></returns>
        public IEnumerable<string> GetFolderPaths(string Title)
        {
            IEnumerable<string> path = null;

            try
            {
                CommonOpenFileDialog folderSelectorDialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    AllowNonFileSystemItems = false,
                    Multiselect = true,
                    Title = Title
                };

                if (folderSelectorDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    path = folderSelectorDialog.FileNames;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

            return path;
        }
        /// <summary>
        /// Open the folder chooser and return the path
        /// </summary>
        /// <param name="IsMultiSelect">Default is false. True = Select 2 or more files</param>
        /// <returns></returns>
        public string GetFolderPath(string Title, bool isMultiSelect = false)
        {
            string path = null;

            try
            {
                CommonOpenFileDialog folderSelectorDialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = true,
                    AllowNonFileSystemItems = false,
                    Multiselect = isMultiSelect,
                    Title = Title
                };

                if (folderSelectorDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    path = folderSelectorDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }

            return path;
        }

        /// <summary>
        /// Get the column
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        /// <summary>
        /// Open the documentation file
        /// </summary>
        public void ShowDocumentation()
        {
            try
            {
                string docFileName = $"{Title}.docx";
                string docPath = Path.Combine("Assets", docFileName);

                if (!File.Exists(docPath))
                {
                    CreatePlaceHolderFile(docPath);
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = docPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                string errorTitle = $"{ApplicationName} - Documentation Error";
                ErrorMessage(ex, errorTitle);
            }
        }

        /// <summary>
        /// Validate if the filepath is being used
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsFileBeingUsed(string filePath)
        {
            bool result = false;

            try
            {
                // Attempt to open the file with specific FileShare options
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    result = false;
                }
            }
            catch (IOException)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reports"></param>
        /// <param name="outFile"></param>
        /// <param name="fontName"></param>
        public void GenerateExcelReportFile<T>(List<T> reports, string outFile, string fontName = "Calibri")
        {
            string titleCell = "A1";
            string fileNameCell = "A2";

            int headersRow = 3;
            int dataRow = headersRow + 1;
            using (ExcelEngine engine = new ExcelEngine())
            {
                IApplication application = engine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                application.DefaultVersion = ExcelVersion.Xlsx;

                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                Debug.WriteLine($"{string.Join(", ", properties.Select(p => FormatPropertyName(p.Name)).ToList())}", $"Model: {typeof(T).Name}");

                #region Column Headers

                List<string> columnHeaders = properties
                    .Select(p => FormatPropertyName(p.Name))
                    .ToList();

                // Title
                worksheet.Range[titleCell].Text = Title;
                worksheet.Range[titleCell].CellStyle.Font.FontName = fontName;

                worksheet.Range[fileNameCell].Text = Path.GetFileName(outFile);
                worksheet.Range[fileNameCell].CellStyle.Font.Bold = true;
                worksheet.Range[fileNameCell].CellStyle.Font.FontName = fontName;

                for (int i = 1; i <= columnHeaders.Count; i++)
                {
                    IRange cell = worksheet[headersRow, i];
                    cell.Text = columnHeaders[i - 1];
                    cell.CellStyle.Font.Bold = true;
                    cell.CellStyle.Font.FontName = fontName;
                    cell.CellStyle.Color = Color.FromArgb(180, 198, 231);
                }

                #endregion

                #region Data

                foreach (T item in reports)
                {
                    for (int col = 1; col <= properties.Length; col++)
                    {
                        var value = properties[col - 1].GetValue(item);
                        worksheet[dataRow, col].Text = value.ToString();
                    }
                    dataRow++;
                }

                #endregion

                worksheet.UsedRange.WrapText = false;
                worksheet.UsedRange.AutofitColumns();
                worksheet.UsedRange.AutofitRows();
                workbook.SaveAs(outFile);
            }
        }
        #endregion

        /// <summary>
        /// Create a placeholder documentation file with basic content.
        /// </summary>
        /// <param name="filePath">The path where the file will be created.</param>
        private void CreatePlaceHolderFile(string docPath)
        {
            try
            {
                using (WordDocument document = new WordDocument())
                {

                    WSection section = document.AddSection() as WSection;

                    IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

                    paragraph = section.AddParagraph();
                    paragraph.ApplyStyle(BuiltinStyle.Heading1);
                    WTextRange textRange = paragraph.AppendText($"Documentation for {ApplicationName}") as WTextRange;

                    paragraph = section.AddParagraph();
                    paragraph.ApplyStyle(BuiltinStyle.Normal);
                    textRange = paragraph.AppendText("This is a placeholder for the documentation file.") as WTextRange;

                    document.Save(docPath);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex, $"{ApplicationName} - Documentation Creation Error");
            }
        }

        private string FormatPropertyName(string propertyName)
        {
            // Replace underscores with spaces
            string formattedName = propertyName.Replace("_", " ");
            // Insert a space between lowercase and uppercase letters
            formattedName = Regex.Replace(formattedName, "(\\p{Ll})(\\p{Lu})", "$1 $2");
            // Return the formatted name
            return formattedName;
        }


    }

    // Efficiently add many items to the ObservableCollection. UI will be less laggy.
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }

}
