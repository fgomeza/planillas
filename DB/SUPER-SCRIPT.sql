﻿CREATE TABLE TCALL_01(
CI_01 BIGSERIAL,
CPR_02 FLOAT,
CONSTRAINT PKCALL_01 PRIMARY KEY (CI_01)
);

CREATE TABLE TERR_01(
CI_01 BIGSERIAL,
CDE_02 TEXT,
CONSTRAINT PKERR_01 PRIMARY KEY (CI_01)
);

CREATE TABLE TSE_01(
CI_01 BIGSERIAL,
CN_02 TEXT,
CONSTRAINT PKSE_01 PRIMARY KEY (CI_01)
);

CREATE TABLE TTR_01(
CI_01 BIGSERIAL,
CC_02 TEXT,
CN_03 TEXT,
CS_04 BIGINT,
CCU_05 TEXT,
CCM_06 BOOL,
CONSTRAINT PKTR_01 PRIMARY KEY(CI_01),
CONSTRAINT FKTR_01 FOREIGN KEY (CS_04) REFERENCES TSE_01
);

CREATE TABLE TC_01(
CTR_01 BIGINT,
CCM_02 TEXT,
CLL_03 BIGINT,
CONSTRAINT PKC_01 PRIMARY KEY (CTR_01),
CONSTRAINT FKC_01 FOREIGN KEY (CTR_01) REFERENCES TTR_01
);

CREATE TABLE TNC_01(
CTR_01 BIGINT,
CSA_02 FLOAT,
CONSTRAINT PKNC_01 PRIMARY KEY(CTR_01),
CONSTRAINT FKNC_01 FOREIGN KEY (CTR_01) REFERENCES TTR_01
);

CREATE TABLE TTDF_01(
CI_01 BIGSERIAL,
CDE_02 TEXT,
CLO_03 BIGINT,
CONSTRAINT PKTDF_01 PRIMARY KEY (CI_01),
CONSTRAINT FKTDF_01 FOREIGN KEY (CLO_03) REFERENCES TSE_01
);

CREATE TABLE TDF_01(
CI_01 BIGSERIAL,
CTR_02 BIGINT,
CDE_03 TEXT,
CMO_04 FLOAT,
CTY_05 BIGINT,
CONSTRAINT PKDF_01 PRIMARY KEY (CI_01),
CONSTRAINT FKDF_01 FOREIGN KEY (CTR_02) REFERENCES TTR_01,
CONSTRAINT FKDF_02 FOREIGN KEY (CTY_05) REFERENCES TTDF_01
);

CREATE TABLE TTDC_01(
CI_01 BIGSERIAL,
CDE_02 TEXT,
CIN_03 FLOAT,
CME_04 BIGINT,
CLO_05 BIGINT,
CONSTRAINT PKTDC_01 PRIMARY KEY (CI_01),
CONSTRAINT FKTDC_01 FOREIGN KEY (CLO_05) REFERENCES TSE_01
);

CREATE TABLE TDC_01(
CI_01 BIGSERIAL,
CTR_02 BIGINT,
CDE_03 TEXT,
CTO_04 FLOAT,
CIN_05 FLOAT,
CCU_06 BIGINT,
CFA_07 BIGINT,
CSA_08 FLOAT,
CTY_09 BIGINT,
CONSTRAINT PKDC_01 PRIMARY KEY (CI_01),
CONSTRAINT FKDC_01 FOREIGN KEY (CTR_02) REFERENCES TTR_01,
CONSTRAINT FKDC_02 FOREIGN KEY (CTY_09) REFERENCES TTDC_01
);

CREATE TABLE TEX_01(
CI_01 BIGSERIAL,
CTR_02 BIGINT,
CDE_03 TEXT,
CMO_04 FLOAT,
CONSTRAINT PKEX_01 PRIMARY KEY (CI_01),
CONSTRAINT FKEX_01 FOREIGN KEY (CTR_02) REFERENCES TTR_01
);

CREATE TABLE TMU_01(
CI_01 BIGSERIAL,
CTR_02 BIGINT,
CDE_03 TEXT,
CMO_04 FLOAT,
CCU_05 BIGINT,
CFA_06 BIGINT,
CSA_07 FLOAT,
CONSTRAINT PKMU_01 PRIMARY KEY (CI_01),
CONSTRAINT FKMU_01 FOREIGN KEY (CTR_02) REFERENCES TTR_01
);

CREATE TABLE TRO_01(
CI_01 BIGSERIAL,
CN_02 TEXT,
CS_03 BIGINT,
CONSTRAINT PKRO_01 PRIMARY KEY (CI_01),
CONSTRAINT FKRO_01 FOREIGN KEY (CS_03) REFERENCES TSE_01
);

CREATE TABLE TGO_01(
CN_01 TEXT,
CDE_02 TEXT,
CIC_03 TEXT,
CAL_04 BOOLEAN,
CONSTRAINT PKGO_01 PRIMARY KEY (CN_01)
);

CREATE TABLE TOP_01(
CN_01 TEXT,
CDE_02 TEXT,
CG_03 TEXT,
CONSTRAINT PKOP_01 PRIMARY KEY (CN_01,CG_03),
CONSTRAINT FKOP_01 FOREIGN KEY (CG_03) REFERENCES TGO_01
);

CREATE TABLE TPRR_01(
CRO_01 BIGINT,
COP_02 TEXT,
CG_03 TEXT,
CONSTRAINT PKPRR_01 PRIMARY KEY (CRO_01,COP_02,CG_03),
CONSTRAINT FKPRR_01 FOREIGN KEY (CRO_01) REFERENCES TRO_01,
CONSTRAINT FKPRR_02 FOREIGN KEY (COP_02,CG_03) REFERENCES TOP_01
);

CREATE TABLE TUS_01(
CI_01 BIGSERIAL,
CN_02 TEXT,
CUS_03 TEXT,
CP_04 TEXT,
CR_05 BIGINT,
CS_06 BIGINT,
CEM_07 TEXT,
CONSTRAINT PKUS_01 PRIMARY KEY (CI_01),
CONSTRAINT FKUS_01 FOREIGN KEY (CR_05) REFERENCES TRO_01,
CONSTRAINT FK2US_01 FOREIGN KEY (CS_06) REFERENCES TSE_01
);


CREATE TABLE TPL_01(
CI_01 BIGSERIAL,
CF_02 DATE,
CUS_03 BIGINT,
CAR_04 TEXT,
CS_05 BIGINT,
CONSTRAINT PKPL_01 PRIMARY KEY (CI_01),
CONSTRAINT FKPL_01 FOREIGN KEY (CS_05) REFERENCES TSE_01
);


--PAYMENT DEBITS
--ADD
CREATE OR REPLACE FUNCTION FDC_01(TR BIGINT,DE TEXT,TOT FLOAT ,INTE FLOAT, MO BIGINT , TYP BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RETURN 1;
	END IF;
	INSERT INTO TDC_01(CTR_02,CDE_03,CTO_04,CIN_05,CCU_06,CFA_07,CSA_08,CTY_09) 
	VALUES(TR,DE,TOT,INTE/100,0,MO,TOT,TYP);
	RETURN 0;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_01(BIGINT,TEXT,FLOAT,FLOAT,BIGINT,BIGINT)
  OWNER TO postgres;


--UPDATE 

CREATE OR REPLACE FUNCTION FDC_02(ID BIGINT,DE TEXT,TOT FLOAT ,INTE FLOAT, MO BIGINT, SA FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TDC_01 WHERE CI_01=ID) THEN
		UPDATE TDC_01 SET CDE_03=DE,CTO_04=TOT,CIN_05=INTE/100,CFA_07=MO, CSA_08=SA WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 2;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_02(BIGINT,TEXT,FLOAT,FLOAT,BIGINT,FLOAT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FDC_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TDC_01 WHERE CI_01=ID) THEN
		DELETE FROM TDC_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 2;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_03(BIGINT)
  OWNER TO postgres;


--SELECT
CREATE OR REPLACE FUNCTION FDC_04(ID BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT,E FLOAT,F BIGINT,G BIGINT,H FLOAT,I BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TDC_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '2';
	END IF;
	RETURN QUERY  SELECT CI_01,CTR_02,CDE_03,CTO_04,CIN_05,CCU_06,CFA_07,CSA_08,CTY_09 FROM TDC_01 WHERE CI_01=ID;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_04(BIGINT)
  OWNER TO postgres;


--SELECT ALL
CREATE OR REPLACE FUNCTION FDC_05(TR BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT,E FLOAT,F BIGINT,G BIGINT,H FLOAT,I BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RAISE EXCEPTION '1';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CTO_04,CIN_05,CCU_06,CFA_07,CSA_08,CTY_09 FROM TDC_01 WHERE CTR_02=TR;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_05(BIGINT)
  OWNER TO postgres;


--PAY DEBIT (AMOUNT)
CREATE OR REPLACE FUNCTION FDC_07(ID BIGINT,AM FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TDC_01 WHERE CI_01=ID) THEN
		RETURN 2;
	END IF;
	IF (SELECT CSA_08<AM FROM TDC_01 WHERE CI_01=ID) THEN
		RETURN 16;
	END IF;
	UPDATE TDC_01 SET CSA_08=CSA_08-AM,CFA_07=CFA_07-1,CCU_06=CCU_06+1 WHERE CI_01=ID;
	RETURN 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDC_07(BIGINT,FLOAT)
  OWNER TO postgres;



--DEBITS
--ADD
CREATE OR REPLACE FUNCTION FDF_01(TR BIGINT,DES TEXT, MO FLOAT,TYP BIGINT)
  RETURNS BIGINT AS
$BODY$
DECLARE
T INT;
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RETURN 1;
	END IF;
	INSERT INTO TDF_01(CTR_02,CDE_03,CMO_04,CTY_05) VALUES(TR,DES,MO,TYP);
	RETURN 0;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDF_01(BIGINT,TEXT,FLOAT,BIGINT)
  OWNER TO postgres;

--UPDATE 

CREATE OR REPLACE FUNCTION FDF_02(ID BIGINT,DES TEXT, MO FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TDF_01 WHERE CI_01=ID) THEN
		UPDATE TDF_01 SET CDE_03=DES,CMO_04=MO WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 2;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDF_02(BIGINT,TEXT,FLOAT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FDF_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TDF_01 WHERE CI_01=ID) THEN
		DELETE FROM TDF_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 2;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDF_03(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FDF_04(ID BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT, E BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TDF_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '2';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04,CTY_05 FROM TDF_01 WHERE CI_01=ID;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDF_04(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FDF_05(TR BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT, E BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RAISE EXCEPTION '2';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04,CTY_05 FROM TDF_01 WHERE CTR_02=TR;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FDF_05(BIGINT)
  OWNER TO postgres;


--EXTRAS

CREATE OR REPLACE FUNCTION FEX_01(TR BIGINT,DE TEXT, AM FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RETURN 1;
	END IF;
	INSERT INTO TEX_01(CTR_02,CDE_03,CMO_04) VALUES(TR,DE,AM);
	RETURN 0;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FEX_01(BIGINT,TEXT,FLOAT)
  OWNER TO postgres;

  
--UPDATE 

CREATE OR REPLACE FUNCTION FEX_02(ID BIGINT,DE TEXT,AM FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TEX_01 WHERE CI_01=ID) THEN
			UPDATE TEX_01 SET CDE_03=DE, CMO_04=AM WHERE CI_01=ID;
			RETURN 0;
	END IF;
	RETURN 3;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FEX_02(BIGINT,TEXT,FLOAT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FEX_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TEX_01 WHERE CI_01=ID) THEN
		DELETE FROM TEX_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 3;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FEX_03(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FEX_04(ID BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TEX_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '3' USING ERRCODE = '3';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04 FROM TEX_01 WHERE CI_01=ID;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FEX_04(BIGINT)
  OWNER TO postgres;
  
-- SELECT ALL (EMPLOYEE)

CREATE OR REPLACE FUNCTION FEX_05(TR BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT, D FLOAT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RAISE EXCEPTION '1';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04 FROM TEX_01 WHERE CTR_02=TR;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FEX_05(BIGINT)
  OWNER TO postgres;


--RECESS
--ADD
CREATE OR REPLACE FUNCTION FMU_01(TR BIGINT,DE TEXT,TOT FLOAT , MO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RETURN 1;
	END IF;
	INSERT INTO TMU_01(CTR_02,CDE_03,CMO_04,CCU_05,CFA_06,CSA_07) VALUES(TR,DE,TOT,0,MO,TOT);
	RETURN 0;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_01(BIGINT,TEXT,FLOAT,BIGINT)
  OWNER TO postgres;

--UPDATE 

CREATE OR REPLACE FUNCTION FMU_02(ID BIGINT,DE TEXT,TOT FLOAT, MO BIGINT, SA FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TMU_01 WHERE CI_01=ID) THEN
		UPDATE TMU_01 SET CDE_03=DE,CMO_04=TOT,CFA_06=MO, CSA_07=SA WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 4;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_02(BIGINT,TEXT,FLOAT,BIGINT,FLOAT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FMU_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TMU_01 WHERE CI_01=ID) THEN
		DELETE FROM TMU_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 4;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_03(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FMU_04(ID BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT,E BIGINT,F BIGINT,G FLOAT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TMU_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '4';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04,CCU_05,CFA_06,CSA_07 FROM TMU_01 WHERE CI_01=ID;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_04(BIGINT)
  OWNER TO postgres;
  

--SELECT ALL
CREATE OR REPLACE FUNCTION FMU_05(TR BIGINT)
  RETURNS TABLE(A BIGINT,B BIGINT,C TEXT,D FLOAT,E BIGINT,F BIGINT,G FLOAT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TTR_01 WHERE CI_01=TR) THEN
		RAISE EXCEPTION '1';
	END IF;
	RETURN QUERY SELECT CI_01,CTR_02,CDE_03,CMO_04,CCU_05,CFA_06,CSA_07 FROM TMU_01 WHERE CTR_02=TR;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_05(BIGINT)
  OWNER TO postgres;


--PAY RECESS (AMOUNT)
CREATE OR REPLACE FUNCTION FMU_07(ID BIGINT,AM FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TMU_01 WHERE CI_01=ID) THEN
		UPDATE TMU_01 SET CSA_07=CSA_07-AM,CFA_06=CFA_06-1,CCU_05=CCU_05+1 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 4;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FMU_07(BIGINT,FLOAT)
  OWNER TO postgres;



--PAYROLL
--ADD
CREATE OR REPLACE FUNCTION FPL_01(F DATE,US BIGINT,FIL TEXT, SE BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TUS_01 WHERE CI_01=US) THEN
		RAISE EXCEPTION '14';
	END IF;
	INSERT INTO TPL_01(CF_02,CUS_03,CAR_04,CS_05) VALUES(F,US,FIL,SE);
	RETURN 0;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_01(DATE,BIGINT,TEXT,BIGINT)
  OWNER TO postgres;


--UPDATE 

CREATE OR REPLACE FUNCTION FPL_02(ID BIGINT,F DATE,US BIGINT,FIL TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TPL_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '17';
	END IF;
	UPDATE TPL_01 SET CF_02=F,CUS_03=US,CAR_04=FIL WHERE CI_01=ID;
	RETURN 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_02(BIGINT,DATE,BIGINT,TEXT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FPL_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TPL_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '17';
	END IF;
	DELETE FROM TPL_01 WHERE CI_01=ID;
	RETURN 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_03(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FPL_04(ID BIGINT)
  RETURNS TABLE(A BIGINT,B DATE,C BIGINT,D TEXT) AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TPL_01 WHERE CI_01=ID) THEN
		RAISE EXCEPTION '17';
	END IF;
	RETURN QUERY SELECT CI_01,CF_02,CUS_03,CAR_04 FROM TPL_01 WHERE CI_01=ID;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_04(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FPL_05(LO BIGINT)
  RETURNS TABLE(A BIGINT,B DATE,C BIGINT,D BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '11';
	END IF;
	RETURN QUERY SELECT CI_01,CF_02,CUS_03,CS_05 FROM TPL_01 WHERE TPL_01.CS_05=LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_05(BIGINT)
  OWNER TO postgres;

--SELECT DATE
CREATE OR REPLACE FUNCTION FPL_06(INI DATE,FIN DATE)
  RETURNS TABLE(A BIGINT,B DATE,C BIGINT,D TEXT) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CI_01,CF_02,CUS_03,CAR_04 FROM TPL_01 WHERE CF_02>=INI AND CF_02<=FIN;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_06(DATE,DATE)
  OWNER TO postgres;

--CALCULATE
CREATE OR REPLACE FUNCTION FPL_07(LO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '11';
	END IF;
	--CALLS
	UPDATE TC_01 SET CLL_03=0 FROM TTR_01 WHERE TC_01.CTR_01=TTR_01.CI_01 AND TTR_01.CS_04=LO;
	--EXTRAS
	DELETE FROM TEX_01 USING TTR_01 WHERE TEX_01.CTR_02=TTR_01.CI_01 AND TTR_01.CS_04=LO;
	--RECESS
	UPDATE TMU_01 SET CSA_07=CSA_07-(CSA_07/CFA_06), CCU_05=TMU_01.CCU_05+1, CFA_06=CFA_06-1 
	FROM TTR_01 WHERE TMU_01.CTR_02=TTR_01.CI_01 AND TTR_01.CS_04=LO;
	--DEBITS
	UPDATE TDC_01 SET CSA_08=CSA_08-(CSA_08/CFA_07), CCU_06=TDC_01.CCU_06+1, CFA_07=CFA_07-1 
	FROM TTR_01 WHERE TDC_01.CTR_02=TTR_01.CI_01 AND TTR_01.CS_04=LO;
	RETURN 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FPL_07(BIGINT)
  OWNER TO postgres;


--PRIVILEGES
--ADD
CREATE OR REPLACE FUNCTION FOP_01(RO BIGINT,OP TEXT,GP TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF  EXISTS (SELECT CI_01 FROM TRO_01 WHERE CI_01=RO) THEN
		IF  EXISTS (SELECT CN_01 FROM TOP_01 WHERE CN_01=OP AND CG_03=GP) THEN
			IF EXISTS (SELECT CRO_01 FROM TPRR_01 WHERE CRO_01=RO AND COP_02=OP AND CG_03=GP) THEN
				RETURN 5;
			END IF;
			INSERT INTO TPRR_01(CRO_01,COP_02,CG_03) VALUES(RO,OP,GP);
			RETURN 0;
		END IF;
		RETURN 6;
		
	END IF;
	RETURN 7;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FOP_01(BIGINT,TEXT,TEXT)
  OWNER TO postgres;


--SELECT ALL (role) NAMES
CREATE OR REPLACE FUNCTION FOP_03(RO BIGINT)
  RETURNS TABLE(A TEXT, B TEXT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TRO_01 WHERE CI_01=RO) THEN
		RAISE EXCEPTION '7';
	END IF;
	RETURN QUERY SELECT TPRR_01.CG_03,TPRR_01.COP_02 FROM TPRR_01 
	WHERE TPRR_01.CRO_01=RO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FOP_03(BIGINT)
  OWNER TO postgres;

  --SELECT ALL
CREATE OR REPLACE FUNCTION FOP_04()
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CN_01,CDE_02,CG_03 FROM TOP_01;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FOP_04()
  OWNER TO postgres;


  --SELECT ALL OPERATIONS(GROUP)
CREATE OR REPLACE FUNCTION FOP_05(GP TEXT)
  RETURNS TABLE(A TEXT,B TEXT) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CN_01,CDE_02 FROM TOP_01 WHERE CG_03=GP;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FOP_05(TEXT)
  OWNER TO postgres;


--SELECT GROUPS
  CREATE OR REPLACE FUNCTION FGO_01()
  RETURNS TABLE(A TEXT,B TEXT,C TEXT,D BOOL) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CN_01,CDE_02,CIC_03,CAL_04 FROM TGO_01;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FGO_01()
  OWNER TO postgres;


--ROLES
--ADD
CREATE OR REPLACE FUNCTION FRO_01(NA TEXT,LO BIGINT)
  RETURNS BIGINT AS
$BODY$
DECLARE
X INT;
BEGIN
	IF EXISTS (SELECT CN_02 FROM TRO_01 WHERE CN_02=NA) THEN
		RAISE EXCEPTION '9';
	ELSE
		IF NOT EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
			RAISE EXCEPTION '11';
		ELSE
			INSERT INTO TRO_01(CN_02,CS_03) VALUES(NA,LO);
			SELECT CI_01 INTO X FROM TRO_01 WHERE CN_02=NA AND CS_03=LO;
			RETURN X;
		END IF;
	END IF;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FRO_01(TEXT,BIGINT)
  OWNER TO postgres;

--UPDATE 

CREATE OR REPLACE FUNCTION FRO_02(ID BIGINT,NA TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TRO_01 WHERE CI_01=ID) THEN
		IF EXISTS (SELECT CN_02 FROM TRO_01 WHERE CN_02=NA AND CI_01!=ID) THEN
			RETURN 9;
		ELSE
			UPDATE TRO_01 SET CN_02=NA WHERE CI_01=ID;
			DELETE FROM TPRR_01 WHERE CRO_01=ID;
			RETURN 0;
		END IF;
	ELSE
		RETURN 7;
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FRO_02(BIGINT,TEXT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FRO_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TRO_01 WHERE CI_01=ID) THEN
		DELETE FROM TPRR_01 WHERE CRO_01=ID;
		DELETE FROM TRO_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 7;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FRO_03(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FRO_04()
  RETURNS TABLE(A BIGINT,B TEXT,C BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS (SELECT CI_01 FROM TSE_01) THEN
		RAISE EXCEPTION '11';
	END IF;
	RETURN QUERY SELECT CI_01,CN_02,CS_03 FROM TRO_01;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FRO_04()
  OWNER TO postgres;

  
--SEDES

--ADD
CREATE OR REPLACE FUNCTION FSE_01(SE TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CN_02 FROM TSE_01 WHERE CN_02=SE) THEN
		RETURN 10;
	END IF;
	INSERT INTO TSE_01(CN_02) VALUES(SE);
	RETURN 0;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FSE_01(TEXT)
  OWNER TO postgres;

--UPDATE
CREATE OR REPLACE FUNCTION FSE_02(ID BIGINT ,SE TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=ID) THEN
		UPDATE TSE_01 SET CN_02=SE WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 11;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FSE_02(BIGINT,TEXT)
  OWNER TO postgres;

  --DELETE
CREATE OR REPLACE FUNCTION FSE_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=ID) THEN
		DELETE FROM TSE_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 11;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FSE_03(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FSE_04()
  RETURNS TABLE(A BIGINT,B TEXT) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CI_01,CN_02 FROM TSE_01;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FSE_04()
  OWNER TO postgres;



--TRABAJADORES
--ADD CMS
CREATE OR REPLACE FUNCTION FTR_01(CD TEXT,CM TEXT,NA TEXT,LO BIGINT,AC TEXT)
  RETURNS BIGINT AS
$BODY$
DECLARE
T BIGINT;
BEGIN
	IF EXISTS (SELECT CC_02 FROM TTR_01 WHERE CC_02=CD) THEN
		RETURN 12;
	END IF;
	IF EXISTS (SELECT CCM_02 FROM TC_01 WHERE CCM_02=CM) THEN
		RETURN 13;
	END IF;
	IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		INSERT INTO TTR_01(CC_02,CN_03,CS_04,CCU_05,CCM_06) VALUES (CD,NA,LO,AC,TRUE);
		SELECT CI_01 INTO T FROM TTR_01 WHERE CC_02=CD;
		INSERT INTO TC_01(CTR_01,CCM_02,CLL_03) VALUES(T,CM,0);
		RETURN 0;
	END IF;
	RETURN 11;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_01(TEXT,TEXT,TEXT,BIGINT,TEXT)
  OWNER TO postgres;
  
  --ADD NON CMS
CREATE OR REPLACE FUNCTION FTR_02(CD TEXT,NA TEXT,LO BIGINT,AC TEXT,SA FLOAT)
  RETURNS BIGINT AS
$BODY$
DECLARE
T BIGINT;
BEGIN
	IF EXISTS (SELECT CC_02 FROM TTR_01 WHERE CC_02=CD) THEN
		RETURN 11;
	END IF;
	IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		INSERT INTO TTR_01(CC_02,CN_03,CS_04,CCU_05,CCM_06) VALUES (CD,NA,LO,AC,FALSE);
		SELECT CI_01 INTO T FROM TTR_01 WHERE CC_02=CD;
		INSERT INTO TNC_01(CTR_01,CSA_02) VALUES(T,SA);
		RETURN 0;
	END IF;
	RETURN 11;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_02(TEXT,TEXT,BIGINT,TEXT,FLOAT)
  OWNER TO postgres;

  --UPDATE CMS
  CREATE OR REPLACE FUNCTION FTR_03(ID BIGINT,CD TEXT,CM TEXT,NA TEXT,LO BIGINT,AC TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	
	IF EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=ID) THEN
		IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
			UPDATE TTR_01 SET CC_02=CD,CN_03=NA,CS_04=LO,CCU_05=AC WHERE CI_01=ID;
			UPDATE TC_01 SET CCM_02=CM WHERE CTR_01=ID;
			RETURN 0;
		END IF;
	RETURN 11;
	END IF;
	RETURN 1;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_03(BIGINT,TEXT,TEXT,TEXT,BIGINT,TEXT)
  OWNER TO postgres;


  --UPDATE NON CMS
  CREATE OR REPLACE FUNCTION FTR_04(ID BIGINT,CD TEXT,NA TEXT,LO BIGINT,AC TEXT,SA FLOAT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=ID) THEN
		IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
			UPDATE TTR_01 SET CC_02=CD,CN_03=NA,CS_04=LO,CCU_05=AC WHERE CI_01=ID;
			UPDATE TNC_01 SET CSA_02=SA WHERE CTR_01=ID;
			RETURN 0;
		END IF;
		RETURN 11;
	END IF;
	RETURN 1;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_04(BIGINT,TEXT,TEXT,BIGINT,TEXT,FLOAT)
  OWNER TO postgres;

  --DELETE
  CREATE OR REPLACE FUNCTION FTR_05(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
DECLARE ISCMS BOOL;
BEGIN

	IF EXISTS (SELECT CI_01 FROM TTR_01 WHERE CI_01=ID) THEN
		SELECT CCM_06 INTO ISCMS FROM TTR_01 WHERE CI_01=ID;
		IF(ISCMS) THEN
			DELETE FROM TC_01 WHERE CTR_01=ID;
		ELSE
			DELETE FROM TNC_01 WHERE CTR_01=ID;
		END IF;
		DELETE FROM TTR_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 1;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_05(BIGINT)
  OWNER TO postgres;

--SELECT
CREATE OR REPLACE FUNCTION FTR_06(ID BIGINT)
  RETURNS TABLE(A TEXT,B TEXT,C TEXT, D TEXT, E BOOL) AS
$BODY$
BEGIN
	IF NOT EXISTS ( SELECT CI_01 FROM TTR_01 WHERE CI_01=ID)THEN
		RAISE EXCEPTION '1';
	END IF;
	RETURN QUERY SELECT TTR_01.CC_02,TTR_01.CN_03,TSE_01.CN_02,TTR_01.CCU_05,TTR_01.CCM_06 FROM TTR_01,TSE_01 
	WHERE TTR_01.CI_01=ID AND TSE_01.CI_01=TTR_01.CS_04;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_06(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FTR_07(LO BIGINT)
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT,D BIGINT, E TEXT, F BOOL) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '11';
	END IF;
	RETURN QUERY SELECT TTR_01.CI_01,TTR_01.CC_02,TTR_01.CN_03,TTR_01.CS_04,TTR_01.CCU_05,TTR_01.CCM_06 FROM TTR_01 
	WHERE TTR_01.CS_04=LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_07(BIGINT)
  OWNER TO postgres;


--SELECT ALL CMS
  CREATE OR REPLACE FUNCTION FTR_08(LO BIGINT)
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT,D BIGINT,E TEXT,F TEXT,G BIGINT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '11';
	END IF;
	RETURN QUERY SELECT TTR_01.CI_01,TTR_01.CC_02,TTR_01.CN_03,TTR_01.CS_04,TTR_01.CCU_05,TC_01.CCM_02,TC_01.CLL_03 FROM TTR_01,TC_01 
	WHERE TC_01.CTR_01=TTR_01.CI_01 AND TTR_01.CS_04=LO;

END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_08(BIGINT)
  OWNER TO postgres;
	
--SELECT ALL NON CMS
  CREATE OR REPLACE FUNCTION FTR_09(LO BIGINT)
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT,D BIGINT,E TEXT,F FLOAT) AS
$BODY$
BEGIN
	IF NOT EXISTS( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '11';
	END IF;
	RETURN QUERY SELECT TTR_01.CI_01,TTR_01.CC_02,TTR_01.CN_03,TTR_01.CS_04,TTR_01.CCU_05,TNC_01.CSA_02 FROM TTR_01,TNC_01 
	WHERE TNC_01.CTR_01=TTR_01.CI_01 AND TTR_01.CS_04=LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_09(BIGINT)
  OWNER TO postgres;

--ADD CALLS

CREATE OR REPLACE FUNCTION FTR_10(TR BIGINT ,LL BIGINT )
  RETURNS BIGINT AS
$BODY$
DECLARE
T TEXT;
BEGIN
	
	IF EXISTS (SELECT CTR_01 FROM TC_01 WHERE CTR_01=TR) THEN
		UPDATE TC_01 SET CLL_03=LL WHERE CTR_01=TR;
		RETURN 0;
	END IF;
	RETURN 1;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTR_10(BIGINT,BIGINT)
  OWNER TO postgres;




--USERS
--ADD
CREATE OR REPLACE FUNCTION FUS_01(NA TEXT,US TEXT,PA TEXT, RO BIGINT,LO BIGINT,EM TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TRO_01 WHERE CI_01=RO) THEN
		IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
			INSERT INTO TUS_01 (CN_02,CUS_03,CP_04,CR_05,CS_06,CEM_07) VALUES(NA,US,PA,RO,LO,EM);
			RETURN 0;
		END IF;
		RETURN 11;
	END IF;
	RETURN 7;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FUS_01(TEXT,TEXT,TEXT,BIGINT,BIGINT,TEXT)
  OWNER TO postgres;

--UPDATE 

CREATE OR REPLACE FUNCTION FUS_02(ID BIGINT,NA TEXT,US TEXT,PA TEXT, RO BIGINT,LO BIGINT,EM TEXT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 TUS_01 WHERE CI_01=ID) THEN
		IF EXISTS (SELECT CI_01 FROM TRO_01 WHERE CI_01=RO) THEN
			IF EXISTS (SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
				UPDATE TUS_01 SET CN_02=NA,CUS_03=US,CP_04=PA,CR_05=RO,CS_06=LO,CEM_07=EM WHERE CI_01=ID;
				RETURN 0;
			END IF;
			RETURN 11;
		END IF;
		RETURN 7;
	END IF;
	RETURN 14;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FUS_02(BIGINT,TEXT,TEXT,TEXT,BIGINT,BIGINT,TEXT)
  OWNER TO postgres;

--DELETE

CREATE OR REPLACE FUNCTION FUS_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS (SELECT CI_01 FROM TUS_01 WHERE CI_01=ID) THEN
		DELETE FROM TUS_01 WHERE CI_01=ID;
		RETURN 0;
	END IF;
	RETURN 14;
	
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FUS_03(BIGINT)
  OWNER TO postgres;

--SELECT 
CREATE OR REPLACE FUNCTION FUS_04(US BIGINT)
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT,D TEXT,E BIGINT,F BIGINT,G TEXT) AS
$BODY$
BEGIN
	IF NOT EXISTS ( SELECT CI_01 FROM TUS_01 WHERE CI_01=US) THEN
		RAISE EXCEPTION '14';
	END IF;
	RETURN QUERY SELECT CI_01,CN_02,CUS_03,CP_04,CR_05,CS_06,CEM_07 FROM TUS_01 WHERE CI_01=US;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FRO_04()
  OWNER TO postgres;



--SELECT ALL
CREATE OR REPLACE FUNCTION FUS_05(LO BIGINT)
  RETURNS TABLE(A BIGINT,B TEXT,C TEXT,D TEXT,E BIGINT,F BIGINT,G TEXT) AS
$BODY$
BEGIN
	IF NOT EXISTS ( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		RAISE EXCEPTION '14';
	END IF;
	RETURN QUERY SELECT CI_01,CN_02,CUS_03,CP_04,CR_05,CS_06,CEM_07 FROM TUS_01 WHERE CS_06=LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FUS_05(BIGINT)
  OWNER TO postgres;


--LOGIN 
CREATE OR REPLACE FUNCTION FUS_06(US TEXT, PA TEXT)
  RETURNS TABLE(A BIGINT) AS
$BODY$
BEGIN

	IF EXISTS( SELECT CI_01 FROM TUS_01 WHERE CUS_03=US) THEN
		IF EXISTS ( SELECT CI_01 FROM TUS_01 WHERE CUS_03=US AND CP_04=PA) THEN
			RETURN QUERY SELECT CI_01 FROM TUS_01 WHERE CUS_03=US AND CP_04=PA;
		ELSE
			RAISE EXCEPTION '15';
		END IF;
	ELSE
		RAISE EXCEPTION '14';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FUS_06(TEXT,TEXT)
  OWNER TO postgres;



--ERRORS
--SELECT
CREATE OR REPLACE FUNCTION FERR_04()
  RETURNS TABLE(A BIGINT,B TEXT) AS
$BODY$
BEGIN
	RETURN QUERY SELECT CI_01, CDE_02 FROM TERR_01;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FERR_04()
  OWNER TO postgres;

--TYPES
--DEBITOS FIJOS
--ADD
CREATE OR REPLACE FUNCTION FTDF_01(NAM TEXT,LO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		IF NOT EXISTS( SELECT CI_01 FROM TTDF_01 WHERE CDE_02=NAM) THEN
			INSERT INTO TTDF_01(CDE_02,CLO_03) VALUES (NAM,LO);
		ELSE
			RAISE EXCEPTION '25';
		END IF;
	ELSE
		RAISE EXCEPTION '11';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDF_01(TEXT,BIGINT)
  OWNER TO postgres;
  
---DELETE
CREATE OR REPLACE FUNCTION FTDF_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS( SELECT CI_01 FROM TTDF_01 WHERE CI_01=ID) THEN
		DELETE FROM TTDF_01 WHERE CI_01 = ID;
	ELSE
		RAISE EXCEPTION '26';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDF_03(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FTDF_04(LO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	SELECT CI_01,CDE_02 FROM TTDF_01 WHERE CLO_03  = LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDF_04(BIGINT)
  OWNER TO postgres;

--DEBITOS A CUOTAS
--ADD
CREATE OR REPLACE FUNCTION FTDC_01(NAM TEXT,INTE FLOAT,ME BIGINT,LO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS( SELECT CI_01 FROM TSE_01 WHERE CI_01=LO) THEN
		IF NOT EXISTS( SELECT CI_01 FROM TTDC_01 WHERE CDE_02=NAM) THEN
			INSERT INTO TTDC_01(CDE_02,CIN_03,CME_04,CLO_05) VALUES (NAM,INTE,ME,LO);
		ELSE
			RAISE EXCEPTION '27';
		END IF;
	ELSE
		RAISE EXCEPTION '11';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDC_01(TEXT,FLOAT,BIGINT,BIGINT)
  OWNER TO postgres;

--UPDATE
CREATE OR REPLACE FUNCTION FTDC_02(ID BIGINT,NAM TEXT,INTE FLOAT,ME BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS( SELECT CI_01 FROM TTDC_01 WHERE CI_01=ID) THEN
		IF NOT EXISTS( SELECT CI_01 FROM TTDC_01 WHERE CDE_02=NAM) THEN
			UPDATE TTDC_01 SET CDE_02=NAM,CIN_03=INTE,CME_04=ME WHERE CI_01=ID;
		ELSE
			RAISE EXCEPTION '27';
		END IF;
	ELSE
		RAISE EXCEPTION '28';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDC_02(BIGINT,TEXT,FLOAT,BIGINT)
  OWNER TO postgres;
  
---DELETE
CREATE OR REPLACE FUNCTION FTDC_03(ID BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	IF EXISTS( SELECT CI_01 FROM TTDC_01 WHERE CI_01=ID) THEN
		DELETE FROM TTDC_01 WHERE CI_01 = ID;
	ELSE
		RAISE EXCEPTION '28';
	END IF;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDC_03(BIGINT)
  OWNER TO postgres;

--SELECT ALL
CREATE OR REPLACE FUNCTION FTDC_04(LO BIGINT)
  RETURNS BIGINT AS
$BODY$
BEGIN
	SELECT CI_01,CDE_02,CIN_03,CME_04 FROM TTDC_01 WHERE CLO_05  = LO;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE;
ALTER FUNCTION FTDC_04(BIGINT)
  OWNER TO postgres;


  
 


