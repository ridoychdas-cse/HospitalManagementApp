USE [HospitalBillingDb]
GO
/****** Object:  StoredProcedure [dbo].[Sp_AllPatienListReport]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_AllPatienListReport]
@ReportType nvarchar(10)=null

as
Begin

if(@ReportType='IN')
begin
select * from OutdoorPatients;
end
else if(@ReportType='OUT')
begin
Select * from [dbo].[IndoorPatients];
end
end

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteBedInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeleteBedInfoById]
	@intId int 
as
Begin
Delete From [dbo].[Beds] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteCabinInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeleteCabinInfoById]
	@intId int 
as
Begin
Delete From [dbo].[Cabins] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteDiagnosisTypeInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeleteDiagnosisTypeInfoById]
	@intId int 
as
Begin
Delete From [dbo].[DiagnosisTypes] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteOtherBillType]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_DeleteOtherBillType]
	@intId int
as
Begin
	UPDATE [dbo].[OtherBillType] SET [Status]=0 Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteOutdoorPatientInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[Sp_DeleteOutdoorPatientInfo]
	@intId int
as
Begin
	Delete From [dbo].[OutdoorPatients] Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeletePackageTypeInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeletePackageTypeInfoById]
	@intId int 
as
Begin
Delete From [dbo].[PackageTypes] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteReferenceBy]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_DeleteReferenceBy]
	@intId int
as
Begin
	UPDATE [dbo].[ReferenceBy] SET [Status]=0 Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteSurgeryInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeleteSurgeryInfoById]
	@intId int 
as
Begin
Delete From [dbo].[Surgerys] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteSurgeryTypeInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_DeleteSurgeryTypeInfoById]
	@intId int 
as
Begin
Delete From [dbo].[SurgeryTypes] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteUserInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_DeleteUserInfo]
	@intId int
as
Begin
	Update [dbo].[Users] Set [Status]=0 Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteUserRoleInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_DeleteUserRoleInfo]
	@intId int
as
Begin
	Update [dbo].[UserRoles]  Set [Status]=0 Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteWardInfoById]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_DeleteWardInfoById]
	@intId int 
as
Begin
Delete From [dbo].[Wards] Where [Id]=@intId;
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_IndoorPatientFinancialReport]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Sp_IndoorPatientFinancialReport]
@startDate nvarchar(50)=null,@endDate nvarchar(50)=null,@BillNo nvarchar(20)=null,@PatientId nvarchar(20)=null,@DiagnosisTypeId int=Null,@DiagnosisId nvarchar(20)=Null,@ReportType nvarchar(10)

as
Begin
if(@ReportType='BWPR')
begin
select t1.EntryDate,t1.PatientId as PtientId,t1.Name as PatientName,t2.DiagnosisBill,t3.SergueryBill,t4.OthersBill,(isnull(t2.DiagnosisBill,0)+isnull(t3.SergueryBill,0)+isnull(t4.OthersBill,0)) as TotallBill,
t5.PayAmount,t5.DIscount,((isnull(t2.DiagnosisBill,0)+isnull(t3.SergueryBill,0)+isnull(t4.OthersBill,0))-(isnull(t5.PayAmount,0)+isnull(t5.DIscount,0))) as Due from 
(select Id,PatientId,Name,EntryDate from IndoorPatients) as t1 
left join (select PatientId,EntryDate,TotalPayableAmount as DiagnosisBill from DiagnosisBillMst Where PatientType='IP') as t2  on t1.Id=t2.PatientId
left join (select PatientId,TotalPayableAmount as SergueryBill from SurgeryBillMst) as t3 on t1.id=t3.PatientId
left join (select PatientId,Sum(Price) as OthersBill from OtherBills group by PatientId) as t4 on t1.Id=t4.PatientId
Left Join (select PatientId,Sum(PayAmount) as PayAmount,sum(SpecialDiscount) as DIscount from MoneyReceiveMst where  PatientType='IP' 
group by PatientId) as t5 on t1.Id=t5.PatientId where ( CONVERT(date,t1.EntryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103)   Or @startDate is Null Or @endDate is null) 
  and (t1.PatientId=@PatientId or @PatientId is null);
end
else if(@ReportType='PWDR')
begin
select PatientId,PatientName,sum(PayableAmount) as PayableAmount,sum(PayAmount) as PayAmount,sum(Due) as Due from View_OutdoorPatientsBillWiseCollectionInfo where (PatientId=@PatientId or @PatientId is null)  group by PatientId,PatientName ;
end

else if(@ReportType='SDR')
begin

select * from  [dbo].[View_IndoorPatientsSurgeryBillInfo]  where (BillNo=@BillNo or @BillNo is null) and  (PatientId=@PatientId or @PatientId is null) and (SurgeryTypeId=@DiagnosisTypeId and @DiagnosisTypeId is null)
 and (SurgeryId=@DiagnosisId or @DiagnosisId is null)
 and ( CONVERT(date,EntryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103)  );
end

else if(@ReportType='DDR')
begin

select * from  [dbo].[View_IndoorPatientsDiagnosisBillInfo]  where
 (BillNo=@BillNo or @BillNo is null) 
 and  (PatientId=@PatientId or @PatientId is null)
  and (DiagnosisTypeId=@DiagnosisTypeId or @DiagnosisTypeId is null)
 and (DiagnosisId=@DiagnosisId or @DiagnosisId is null)
 and
  ( CONVERT(date,EntryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103)  );
end


end
GO
/****** Object:  StoredProcedure [dbo].[Sp_OutdoorPatientFinancialReport]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Sp_OutdoorPatientFinancialReport]
@startDate nvarchar(50)=null,@endDate nvarchar(50)=null,@BillNo nvarchar(20)=null,@PatientId nvarchar(20)=null,@DiagnosisTypeId nvarchar(20)=null,@DiagnosisId nvarchar(20)=Null,@ReportType nvarchar(10)

as
Begin
if(@ReportType='BWDR')
begin
SELECT EntryDate,BillNo,PatientId,PatientName,PayableAmount,PayAmount,Discount,Due FROM [View_OutdoorPatientsBillWiseCollectionInfo] where  (BillNo=@BillNo or @BillNo is null) and  (PatientId=@PatientId or @PatientId is null) 
 AND ( CONVERT(date,EntryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103)   Or @startDate is Null Or @endDate is null);;
end
else if(@ReportType='PWDR')
begin
select PatientId,PatientName,sum(PayableAmount) as PayableAmount,sum(PayAmount) as PayAmount,sum(Due) as Due from View_OutdoorPatientsBillWiseCollectionInfo where (PatientId=@PatientId or @PatientId is null)  group by PatientId,PatientName ;
end

else if(@ReportType='DDR')
begin

select * from  [dbo].[View_OutdoorPatientsBillInfo]  where (BillNo=@BillNo or @BillNo is null) AND  (PatientId=@PatientId or @PatientId is null) AND (DiagnosisTypeId=@DiagnosisTypeId or @DiagnosisTypeId is null)
 and (DiagnosisId=@DiagnosisId or @DiagnosisId is null)
 AND ( CONVERT(date,EntryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103)   Or @startDate is Null Or @endDate is null);
end


end
GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveBedInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SaveBedInfo]
	@strName varchar(100) NULL,
	@intWardId int null,
	@dcmlPriceDaily decimal(18,0) null,
	@blnStatus bit NULL
as
Begin

INSERT INTO [dbo].[Beds] ([Name], [WardId], [PriceDaily], [Status])
VALUES(@strName,@intWardId, @dcmlPriceDaily,@blnStatus)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveCabinInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SaveCabinInfo]
	@strName varchar(100) NULL,
	@intRoomTypeId int null,
	@intFloorId int null,
	@dcmlPriceDaily decimal(18,0) null,
	@bitStatus bit NULL
as
Begin
INSERT INTO [dbo].[Cabins]( [Name], [RoomTypeId],[FloorId], [PriceDaily], [Status])
VALUES(@strName,@intRoomTypeId,@intFloorId,@dcmlPriceDaily,@bitStatus)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveDiagnosisTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SaveDiagnosisTypeInfo]
	@strName varchar(100) NULL,
	@strShortName varchar(50) null,
	@strDetails varchar(max) NULL
as
Begin

INSERT INTO [dbo].[DiagnosisTypes] ( [Name], [ShortName], [Details]) 
VALUES (@strName,@strShortName,@strDetails)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveHospitalDepartment]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SaveHospitalDepartment]
	@strName varchar(200) NULL,
	@strShortName varchar(20) NULL,
	@strDetails varchar (max) NULL
as
Begin
INSERT INTO [dbo].[HospitalDepartments](Name, ShortName, Details)
Values(@strName,@strShortName,@strDetails);
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveMoneyReceiveDtl]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Sp_SaveMoneyReceiveDtl]
	@intMoneyReceiveMstId INT = NULL,
	@strPayMethode VARCHAR(50) = NULL,
	@strBankName NVARCHAR(50) = NULL, 
	@strChequeNo VARCHAR(50) = NULL,
	@dtmChequeDate DATETIME = NULL,
	@dtmEntryDate DATETIME = NULL,
	@strMoneyReceiveBy VARCHAR(100) = NULL
as
Begin 
	INSERT INTO [dbo].[MoneyReceiveDtl] ([MoneyReceiveMstId], [PayMethode], [BankName], [ChequeNo], [ChequeDate], [EntryDate], [MoneyReceiveBy]) 
	VALUES (@intMoneyReceiveMstId, @strPayMethode, @strBankName, @strChequeNo, @dtmChequeDate, @dtmEntryDate, @strMoneyReceiveBy)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveMoneyReceiveMst]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Sp_SaveMoneyReceiveMst]
	@intPatientId INT = NULL,
	@strPatientType VARCHAR(50) = NULL,
	@intDiagnosisBillMstId INT = NULL,
	@dcmlPayAmount DECIMAL(18,0) = NULL,
	@dcmlAdvanceAmount DECIMAL(18,0) = NULL,
	@dcmlSpecialDiscount DECIMAL (18,0) = NULL
as
Begin 
	INSERT INTO [dbo].[MoneyReceiveMst]([PatientId], [PatientType], [DiagnosisBillMstId], [PayAmount], [AdvanceAmount], [SpecialDiscount])
	VALUES (@intPatientId, @strPatientType, @intDiagnosisBillMstId, @dcmlPayAmount, @dcmlAdvanceAmount, @dcmlSpecialDiscount)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveOrganizationInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_SaveOrganizationInfo]
	@strName varchar(200) NULL,
	@strShortName varchar(20) NULL,
	@binImage varbinary (max) NULL,
	@strPhoneNo varchar (25) NULL,
	@strEmail varchar(200) NULL,
	@strAddress varchar (max) NULL,
	@strOrganizationSpeech varchar (200) NULL,
	@strDescription varchar (max) NULL,
	@bitStatus bit NULL
as
Begin

INSERT INTO [dbo].[Organization] (Name, ShortName,Image, PhoneNo, Email, Address, OrganizationSpeech, Description, Status)
Values(@strName,@strShortName,@binImage,@strPhoneNo,@strEmail,@strAddress,@strOrganizationSpeech,@strDescription,@bitStatus);
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveOtherBillType]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_SaveOtherBillType]
	@strName varchar(255) NULL,
	@blnStatus bit NULL
as
Begin
	INSERT INTO OtherBillType([Name], [Status]) VALUES(@strName,@blnStatus)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveOutdoorPatientInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_SaveOutdoorPatientInfo]
	@strPatientId varchar(100) NULL, 
	@strName varchar(100) NULL, 
	@strPhoneNo varchar(15) NULL, 
	@strGender varchar(8) NULL,
	@strAge varchar(3) NULL,
	@dtmEntryDate date NULL,
	@intDepartmentId int NULL,
	@intDoctorId int NULL, 
	@intReferenceById int NULL,
	@intDivisionId int NULL, 
	@intDistrictId int NULL, 
	@intThanaId int NULL
as
Begin 
	INSERT INTO [dbo].[OutdoorPatients]
	(
	 [PatientId],
	 [Name],
	 [PhoneNo],
	 [Gender],
	 [Age],
	 [EntryDate],
	 [DepartmentId],
	 [DoctorId],
	 [ReferenceById],
	 [DivisionId],
	 [DistrictId], 
	 [ThanaId]
	 )
	VALUES
	(
	@strPatientId, 
	@strName , 
	@strPhoneNo, 
	@strGender,
	@strAge,
	@dtmEntryDate,
	@intDepartmentId,
	@intDoctorId, 
	@intReferenceById,
	@intDivisionId, 
	@intDistrictId, 
	@intThanaId 
	)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SavePackageTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SavePackageTypeInfo]
	@strName varchar(200) NULL,
	@strShortName varchar(50) null,
	@strDescription varchar(max) NULL
as
Begin
	INSERT INTO [dbo].[PackageTypes] ([Name], [ShortName],[Description])
	VALUES(@strName,@strShortName,@strDescription);
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveReferenceBy]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_SaveReferenceBy]
	@strName varchar(100) NULL
as
Begin
	INSERT INTO [dbo].[ReferenceBy]([Name],[Status]) VALUES(@strName,0)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveSurgeryInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_SaveSurgeryInfo]
	@strName varchar(100) NULL,
	@strShortName varchar(10) null,
	@intSurgeryTypeId int null,
	@dcmlRegularFee decimal(18,0),
	@dcmlDiscount decimal(18,0),
	@dcmlTotalFee decimal(18,0),
	@strDescription varchar(max) NULL
as
Begin
	INSERT INTO [dbo].[Surgerys]([Name], [ShortName], [SurgeryTypeId], [RegularFee], [Discount], [TotalFee], [Description])
	VALUES(@strName,@strShortName,@intSurgeryTypeId,@dcmlRegularFee,@dcmlDiscount,@dcmlTotalFee,@strDescription);
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveSurgeryTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_SaveSurgeryTypeInfo]
	@strName varchar(100) NULL,
	@strShortName varchar(50) null,
	@strDescription varchar(max) NULL
as
Begin
	INSERT INTO [dbo].[SurgeryTypes] ([Name], [ShortName], [Description])
	VALUES (@strName,@strShortName,@strDescription)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveUserInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_SaveUserInfo]
	@strFirstName varchar(100) NULL, 
	@strLastName varchar(50) NULL, 
	@strPhoneNo varchar(15) NULL, 
	@strUserName varchar(50) NULL, 
	@strPassword varchar(50) NULL, 
	@intUserRoleId int
as
Begin
	INSERT INTO [dbo].[Users]([FirstName], [LastName], [PhoneNo], [UserName], [Password], [UserRoleId], [Status])
	VALUES(@strFirstName,@strLastName,@strPhoneNo,@strUserName,@strPassword,@intUserRoleId,1)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveUserRoleInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_SaveUserRoleInfo]
	@strName varchar(50) NULL, 
	@strDescription varchar(max) NULL
as
Begin 
	INSERT INTO [dbo].[UserRoles]([Name], [Description], [Status])
	VALUES(@strName,@strDescription,1)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_SaveWardInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_SaveWardInfo]
	@strName varchar(100) NULL,
	@intDepartmentId int null,
	@intFloorId int null,
	@strWardFor varchar(50) NULL,
	@intRoomTypeId int null,
	@strDetails varchar (max) NULL
as
Begin
Insert Into [dbo].[Wards]([Name], [DepartmentId], [FloorId], [WardFor], [RoomTypeId], [Details])
Values(@strName, @intDepartmentId,@intFloorId,@strWardFor, @intRoomTypeId, @strDetails)
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_TotalDiagnosisBillSummery]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_TotalDiagnosisBillSummery]
@startDate nvarchar(50)=null,@endDate nvarchar(50)=null,@PatientType nvarchar(10)=null
as
Begin
select t3.Name as DiagnosisType,t4.Name as DiagnosisName,count(t4.Name) as TotalDiagnosis ,sum(t1.Price) as Price,sum(t1.Discount) as Discount,sum(t1.PayableAmount) as TotalPayableAmount from DiagnosisBillDtl t1 
Inner join DiagnosisBillMst t2 on t1.DiagnosisBillMstId=t2.Id
Inner join DiagnosisTypes t3 on t1.DiagnosisTypeId=t3.Id 
Inner join Diagnosis t4 on t1.DiagnosisId=t4.Id
where CONVERT(date,t1.DeliveryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103) and (PatientType=@PatientType or @PatientType is null)
group by t3.Name ,t4.Name 
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TotalDiagnosisSummery]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_TotalDiagnosisSummery]
@startDate nvarchar(50)=null,@endDate nvarchar(50)=null,@PatientType nvarchar(10)=null
as
Begin
select t3.Name as DiagnosisType,t4.Name as DiagnosisName,sum(t1.Price) as Price,sum(t1.Discount) as Discount,sum(t1.PayableAmount) as TotalPayableAmount from DiagnosisBillDtl t1 
Inner join DiagnosisBillMst t2 on t1.DiagnosisBillMstId=t2.Id
Inner join DiagnosisTypes t3 on t1.DiagnosisTypeId=t3.Id 
Inner join Diagnosis t4 on t1.DiagnosisId=t4.Id
where CONVERT(date,t1.DeliveryDate,103) between CONVERT(date,@startDate,103) and  CONVERT(date,@endDate,103) and (PatientType=@PatientType or @PatientType is null)
group by t3.Name ,t4.Name 
End


GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateBedInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_UpdateBedInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@intWardId int null,
	@dcmlPriceDaily decimal(18,0) null,
	@blnStatus bit NULL
as
Begin
UPDATE [dbo].[Beds] SET [Name]=@strName, [WardId]=@intWardId, [PriceDaily]=@dcmlPriceDaily, [Status]=@blnStatus
WHERE Id=@intId
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateCabinInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_UpdateCabinInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@intRoomTypeId int null,
	@intFloorId int null,
	@dcmlPriceDaily decimal(18,0) null,
	@bitStatus bit NULL
as
Begin
UPDATE [dbo].[Cabins] SET [Name]=@strName, [RoomTypeId]=@intRoomTypeId,[FloorId]=@intFloorId, [PriceDaily]=@dcmlPriceDaily, [Status]=@bitStatus
WHERE Id=@intId
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateDiagnosisTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_UpdateDiagnosisTypeInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@strShortName varchar(50) null,
	@strDetails varchar(max) NULL
as
Begin
	Update [dbo].[DiagnosisTypes] SET [Name]=@strName, [ShortName]=@strShortName, [Details]=@strDetails
	Where Id=@intId 
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateHospitalDepartment]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create  proc [dbo].[Sp_UpdateHospitalDepartment]
	@intId int,
	@strName varchar(200) NULL,
	@strShortName varchar(20) NULL,
	@strDetails varchar (max) NULL
as
Begin
Update [dbo].[HospitalDepartments] Set Name=@strName, ShortName=@strShortName, Details=@strDetails
Where Id=@intId
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateOtherBillType]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[Sp_UpdateOtherBillType]
	@intId int,
	@strName varchar(255) NULL,
	@blnStatus bit NULL
as
Begin
	Update [dbo].[OtherBillType] Set [Name]=@strName, [Status]=@blnStatus Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateOutdoorPatientInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[Sp_UpdateOutdoorPatientInfo]
	@intId Int,
	@strPatientId varchar(100) NULL, 
	@strName varchar(100) NULL, 
	@strPhoneNo varchar(15) NULL, 
	@strGender varchar(8) NULL,
	@strAge varchar(3) NULL,
	@dtmEntryDate date NULL,
	@intDepartmentId int NULL,
	@intDoctorId int NULL, 
	@intReferenceById int NULL,
	@intDivisionId int NULL, 
	@intDistrictId int NULL, 
	@intThanaId int NULL
as
Begin 
	Update [dbo].[OutdoorPatients] Set
	 [PatientId]=@strPatientId,
	 [Name]=@strName,
	 [PhoneNo]=@strPhoneNo,
	 [Gender]=@strGender,
	 [Age]=@strAge,
	 [EntryDate]=@dtmEntryDate,
	 [DepartmentId]=@intDepartmentId,
	 [DoctorId]=@intDoctorId,
	 [ReferenceById]=@intReferenceById,
	 [DivisionId]=@intDivisionId,
	 [DistrictId]=@intDistrictId, 
	 [ThanaId]=@intThanaId
	 Where [Id]=@intId
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdatePackageTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_UpdatePackageTypeInfo]
	@intId int ,
	@strName varchar(200) NULL,
	@strShortName varchar(50) null,
	@strDescription varchar(max) NULL
as
Begin
	Update [dbo].[PackageTypes] SET [Name]=@strName, [ShortName]=@strShortName,[Description]=@strDescription
	Where Id=@intId 
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateReferenceBy]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_UpdateReferenceBy]
	@intId int,
	@strName varchar(255) NULL
as
Begin
	Update [dbo].[ReferenceBy] Set [Name]=@strName Where [Id]=@intId
End
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateSurgeryInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_UpdateSurgeryInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@strShortName varchar(10) null,
	@intSurgeryTypeId int null,
	@dcmlRegularFee decimal(18,0),
	@dcmlDiscount decimal(18,0),
	@dcmlTotalFee decimal(18,0),
	@strDescription varchar(max) NULL
as
Begin
	Update [dbo].[Surgerys] SET
	[Name]=@strName, [ShortName]=@strShortName, [SurgeryTypeId]=@intSurgeryTypeId, [RegularFee]=@dcmlRegularFee,
	[Discount]=@dcmlDiscount, [TotalFee]=@dcmlTotalFee, [Description]=@strDescription
	Where Id=@intId 
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateSurgeryTypeInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  proc [dbo].[Sp_UpdateSurgeryTypeInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@strShortName varchar(50) null,
	@strDescription varchar(max) NULL
as
Begin
	Update [dbo].[SurgeryTypes] SET [Name]=@strName, [ShortName]=@strShortName, [Description]=@strDescription
	Where Id=@intId 
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Sp_UpdateUserInfo]
	@intId int,
	@strFirstName varchar(100) NULL, 
	@strLastName varchar(50) NULL, 
	@strPhoneNo varchar(15) NULL, 
	@strUserName varchar(50) NULL, 
	@strPassword varchar(50) NULL, 
	@intUserRoleId int
as
Begin
	Update [dbo].[Users] Set [FirstName]=@strFirstName, [LastName]=@strLastName,
							 [PhoneNo]=@strPhoneNo, [UserName]=@STRUSERNAME, [Password]=@strPassword,
							 [UserRoleId]=@intUserRoleId Where [Id]=@intId
	
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateUserRoleInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Sp_UpdateUserRoleInfo]
	@intId int,
	@strName varchar(50) NULL, 
	@strDescription varchar(max) NULL
as
Begin
	Update [dbo].[UserRoles]  Set [Name]=@strName, [Description]=@strDescription Where [Id]=@intId	
End

GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateWardInfo]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE  proc [dbo].[Sp_UpdateWardInfo]
	@intId int ,
	@strName varchar(100) NULL,
	@intDepartmentId int null,
	@intFloorId int null,
	@strWardFor varchar(50) NULL,
	@intRoomTypeId int null,
	@strDetails varchar (max) NULL
as
Begin
UPDATE [dbo].[Wards] SET [Name]=@strName, [DepartmentId]=@intDepartmentId, [FloorId]=@intFloorId, [WardFor]=@strWardFor, [RoomTypeId]=@intRoomTypeId, [Details]=@strDetails
WHERE [Id]=@intId
End

GO
/****** Object:  StoredProcedure [dbo].[spGetImage]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGetImage]
@id int
as
Begin
 select Image from Organization Where Id=@id
End


GO
/****** Object:  StoredProcedure [dbo].[spUploadImage]    Script Date: 5/22/2026 5:01:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spUploadImage]
@ImageData varbinary(MAX)
as
Begin
 update Organization set Image=@ImageData Where Id=2
End


GO
