using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;

namespace FBTS.Library.Common
{
    public class ExportData
    {
        public void VbExportExcel(GridView argsGrid, string argsFileName, string argsTitle)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", argsFileName.Trim()));
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            var sw = new StringWriter();
            var hw = new HtmlTextWriter(sw);

            //Change the Header Row back to white color
            argsGrid.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            foreach (TableCell cll in argsGrid.HeaderRow.Cells)
            {
                cll.Style.Add("background-color", "#cccccc");
            }

            for (int i = 0; i <= argsGrid.Rows.Count - 1; i++)
            {
                GridViewRow row = argsGrid.Rows[i];
                //Change Color back to white
                row.BackColor = Color.White;
                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");
                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    foreach (TableCell cll in row.Cells)
                    {
                        cll.Style.Add("background-color", "#cccccc");
                    }
                }
            }
            if (!string.IsNullOrEmpty(argsTitle))
            {
                var lblTitle = new Label {Text = argsTitle, CssClass = "lblRptHead"};
                lblTitle.RenderControl(hw);
            }

            argsGrid.RenderControl(hw);
            //style to format numbers to string
            const string style = "<style> .textmode { mso-number-format:\\@; } .lblRptHead{\tfont-weight:bold !important; font-size:14pt; color:#074676;\tborder: 1;\twidth:100%;\tpadding:5px 5px 5px 5px;}</style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        public void VbExportPdf(GridView argsGrid, string argsFileName, string argsTitle)
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + argsFileName.Trim() + ".pdf");

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            var sw = new StringWriter();
            var hw = new HtmlTextWriter(sw);

            argsGrid.HeaderRow.Style.Add("background-color", "gray");
            if (!string.IsNullOrEmpty(argsTitle))
            {
                var lblTitle = new Label {Text = argsTitle};
                lblTitle.RenderControl(hw);
            }
            argsGrid.RenderControl(hw);

            var sr = new StringReader(sw.ToString());
            var pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 7f, 7f, 7f, 0f);
            var htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.End();

        }
    }
}
