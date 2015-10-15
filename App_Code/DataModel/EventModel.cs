using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Event Model Class
/// </summary>
public class EventModel
{
	public EventModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Gets or sets member of UID
    /// </summary>
    public int UID { get; set; }

    /// <summary>
    /// Gets or sets member of subject
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets member of detail
    /// </summary>
    public string Detail { get; set; }

    /// <summary>
    /// Gets or sets member of picture thumbnail size
    /// </summary>
    public string PicThumbnail { get; set; }

    /// <summary>
    /// Gets or sets member of picture full size
    /// </summary>
    public string PicFull { get; set; }

    /// <summary>
    /// Gets or sets member of department UID
    /// </summary>
    public int DepartmentUID { get; set; }

    /// <summary>
    /// Gets or sets member of active date from
    /// </summary>
    public DateTime ActiveDateFrom { get; set; }

    /// <summary>
    /// Gets or sets member of active date to
    /// </summary>
    public DateTime ActiveDateTo { get; set; }

    /// <summary>
    /// Gets or sets member of Remark
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// Gets or sets member of time to create record
    /// </summary>
    public DateTime CWhen { get; set; }

    /// <summary>
    /// Gets or sets member of user who create record
    /// </summary>
    public int CUser { get; set; }

    /// <summary>
    /// Gets or sets member of time to modify record
    /// </summary>
    public DateTime MWhen { get; set; }

    /// <summary>
    /// Gets or sets member of user who modify record
    /// </summary>
    public int MUser { get; set; }

    /// <summary>
    /// Gets or sets member of status flag
    /// </summary>
    public string StatusFlag { get; set; }

    /// <summary>
    /// Gets or sets member of language
    /// </summary>
    public int LanguageUID { get; set; }
}