
GO
/****** Object:  View [dbo].[View_OutdoorPatientsBillInfo]    Script Date: 5/22/2026 4:59:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[View_OutdoorPatientsBillInfo] as 

select T1.Id,t1.EntryDate,T1.BillNo,T1.PatientType ,T1.PatientId as PatientMainId,T2.PatientId ,T2.Name as PatientName,T3.DiagnosisTypeId,T4.Name AS DiagnosisType,T3.DiagnosisId,T5.Name AS Diagnosis,ISNULL(T3.Price,0) as Price,ISNULL(T3.Discount,0) as Discount,ISNULL(T3.PayableAmount,0) as PayableAmount  from DiagnosisBillMst T1 
INNER JOIN OutdoorPatients T2 ON T1.PatientId=T2.Id
Left JOIN DiagnosisBillDtl T3 ON T1.ID=T3.DiagnosisBillMstId
Left join DiagnosisTypes T4 on T3.DiagnosisTypeId=T4.Id 
Left join Diagnosis t5 on T3.DiagnosisId=T5.Id
where T1.PatientType='OP'


GO
/****** Object:  View [dbo].[View_OutdoorPatientsBillWiseCollectionInfo]    Script Date: 5/22/2026 4:59:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*drop view View_OutdoorPatientsBillCollectionInfo
Bill Wise Report
 select PatientId,PatientName,sum(PayableAmount) as PayableAmount,sum(PayAmount) as PayAmount,sum(Due) as Due from View_OutdoorPatientsBillWiseCollectionInfo group by PatientId,PatientName*/
CREATE VIEW [dbo].[View_OutdoorPatientsBillWiseCollectionInfo]
AS

SELECT T1.EntryDate, T1.BillNo, T1.PatientId, T1.PatientName, T1.PayableAmount, ISNULL(T2.PayAmount, 0) AS PayAmount,Isnull(T2.Discount,0) as Discount,Isnull(T1.PayableAmount,0)-(Isnull(T2.Discount,0) + (Isnull(T2.PayAmount,0))) AS Due
FROM (SELECT        Id, EntryDate, BillNo, PatientId, PatientName, SUM(PayableAmount) AS PayableAmount
                          FROM            dbo.View_OutdoorPatientsBillInfo
                          GROUP BY Id, EntryDate, BillNo, PatientId, PatientName) AS T1 LEFT OUTER JOIN
                             (SELECT        DiagnosisBillMstId, SUM(SpecialDiscount) as Discount,SUM(IPVat) as Vat,SUM(PayAmount) AS PayAmount
                               FROM            dbo.MoneyReceiveMst
                               WHERE        (PatientType = 'OP')
                               GROUP BY DiagnosisBillMstId) AS T2 ON T1.Id = T2.DiagnosisBillMstId

GO
/****** Object:  View [dbo].[View_IndoorPatientsDiagnosisBillInfo]    Script Date: 5/22/2026 4:59:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create VIEW [dbo].[View_IndoorPatientsDiagnosisBillInfo] as 

select T1.Id,t1.EntryDate,T1.BillNo,T1.PatientType ,T1.PatientId as PatientMainId,T2.PatientId ,T2.Name as PatientName,T3.DiagnosisTypeId,
T4.Name AS DiagnosisType,T3.DiagnosisId,T5.Name AS Diagnosis,ISNULL(T3.Price,0) as Price,ISNULL(T3.Discount,0) as Discount,
ISNULL(T3.PayableAmount,0) as PayableAmount
from DiagnosisBillMst T1 
INNER JOIN IndoorPatients T2 ON T1.PatientId=T2.Id
Left JOIN DiagnosisBillDtl T3 ON T1.ID=T3.DiagnosisBillMstId
Left join DiagnosisTypes T4 on T3.DiagnosisTypeId=T4.Id 
Left join Diagnosis t5 on T3.DiagnosisId=T5.Id
where T1.PatientType='IP'




GO
/****** Object:  View [dbo].[View_IndoorPatientsSurgeryBillInfo]    Script Date: 5/22/2026 4:59:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[View_IndoorPatientsSurgeryBillInfo] as 

select T1.Id,t1.EntryDate,T1.BillNo,T1.PatientType ,T1.PatientId as PatientMainId,T2.PatientId ,T2.Name as PatientName,T3.SurgeryTypeId,
T4.Name AS SurgeryType,T3.SurgeryId,T5.Name AS Surgery,ISNULL(T3.Price,0) as Price,ISNULL(T3.Discount,0) as Discount,
ISNULL(T3.PayableAmount,0) as PayableAmount
from SurgeryBillMst T1 
INNER JOIN IndoorPatients T2 ON T1.PatientId=T2.Id
Left JOIN SurgeryBillDtl T3 ON T1.ID=T3.SurgeryBillMstId
Left join SurgeryTypes T4 on T3.SurgeryTypeId=T4.Id 
Left join Surgerys t5 on T3.SurgeryId=T5.Id
where T1.PatientType='IP'





GO
