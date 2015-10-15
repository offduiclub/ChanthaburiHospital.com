using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventSQL
/// </summary>
public class EventSQL
{
	public EventSQL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// SQL statement for search all event
    /// </summary>
    public static readonly string SelectEventAll = @"SELECT [UID] ,[Subject] ,[Detail] ,[PicThumbnail] ,[PicFull] 
 ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID]
  FROM [Event]  WHERE [StatusFlag] = 'A'  ORDER BY UID DESC";

    /// <summary>
    /// SQL statement for search event by UID
    /// </summary>
    public static readonly string SelectEventByUID = @"SELECT [UID] ,[Subject] ,[Detail] ,[PicThumbnail] ,[PicFull] 
 ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID]
 FROM [Event] WHERE [StatusFlag] = 'A' AND [UID] = @UID
 ORDER BY UID DESC";

    /// <summary>
    /// SQL statement for search event by subject
    /// </summary>
    public static readonly string SelectEventBySubject = @"SELECT [UID] ,[Subject] ,[Detail] ,[PicThumbnail] ,[PicFull] 
 ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID]
 FROM [Event] WHERE [StatusFlag] = 'A' AND [Subject] like @Subject
 ORDER BY UID DESC";

    /// <summary>
    /// SQL statement for search event by department
    /// </summary>
    public static readonly string SelectEventByDepartment = @"";

    /// <summary>
    /// SQL statement for search event by date
    /// </summary>
    public static readonly string SelectEventByDate = @"SELECT [UID] ,[Subject] ,[Detail] ,[PicThumbnail] ,[PicFull] 
 ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID]
 FROM [Event] WHERE [StatusFlag] = 'A' AND @EventDate 
 between [ActiveDateFrom] and [ActiveDateTo] ORDER BY UID DESC";

    /// <summary>
    /// SQL statement for search event by status flag
    /// </summary>
    public static readonly string SelectEventByStatusFlag = @"SELECT [UID] ,[Subject] ,[Detail] ,[PicThumbnail] ,[PicFull] 
 ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID]
 FROM [Event] WHERE [StatusFlag] = @StatusFlag
 ORDER BY UID DESC";

    /// <summary>
    /// SQL statement for search event by Language
    /// </summary>
    public static readonly string SelectEventByLanguage = @"SELECT e.[UID] ,e.[Subject] ,e.[Detail] ,e.[PicThumbnail] ,e.[PicFull] 
 ,e.[DepartmentUID] ,e.[ActiveDateFrom] ,e.[ActiveDateTo] ,e.[Remark]
 ,e.[CWhen] ,e.[CUser] ,e.[MWhen] ,e.[MUser] ,e.[StatusFlag] ,l.[LanguageUID],l.[Detail]
 FROM [Event] e INNER JOIN [Language] l on e.languageUID = l.uid WHERE e.[LanguageUID] = @LanguageUID
 ORDER BY e.UID DESC";

    /// <summary>
    /// SQL statement for insert event
    /// </summary>
    public static readonly string InsertEvent = @"INSERT INTO [Event] ([Subject] ,[Detail] ,[PicThumbnail]
 ,[PicFull] ,[DepartmentUID] ,[ActiveDateFrom] ,[ActiveDateTo] ,[Remark]
 ,[CWhen] ,[CUser] ,[MWhen] ,[MUser] ,[StatusFlag] ,[LanguageUID])
 VALUES (@Subject ,@Detail ,@PicThumbnail ,@PicFull ,@DepartmentUID
 ,@ActiveDateFrom ,@ActiveDateTo ,@Remark,getdate() ,@CUser
 ,getdate() ,@MUser ,@StatusFlag ,@LanguageUID)";

    /// <summary>
    /// SQL statement for delete event
    /// </summary>
    public static readonly string DeleteEvent = @"DELETE FROM [Event] WHERE [UID] = @UID";
}