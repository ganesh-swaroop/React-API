using System;
using System.ComponentModel.DataAnnotations;

public class CommonCodesSO
{
    public string CodeType { get; set; }
    public string[] CodeTypeArr { get; set; }
    public string CMCode { get; set; }
    public string[] CMCodeArr { get; set; }
    public string CDDesc { get; set; }
    public string[] CDDescArr { get; set; }
    public string IsSysParam { get; set; }
    public string[] IsSysParamArr { get; set; }
    public string IsActive { get; set; }
    public string[] IsActiveArr { get; set; }
    public string CreID { get; set; }
    public string[] CreIDArr { get; set; }
    public DateTime CreTime { get; set; }
    public DateTime ModTime { get; set; }
    public string ModID { get; set; }
    public string[] ModIDArr { get; set; }
    public string Orderby { get; set; }
    public int StartIndex { get; set; }
    public int RecordsPerPage { get; set; }

}