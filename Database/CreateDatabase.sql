DROP TABLE IF EXISTS "TextContents";
DROP TABLE IF EXISTS "SectionsContentElements";
DROP TABLE IF EXISTS "Sections";
DROP TABLE IF EXISTS "Documents";
DROP TABLE IF EXISTS "ContentElements";
DROP TABLE IF EXISTS "Languages";

CREATE TABLE "Languages"
(
	"LanguageId" varchar(10) NOT NULL,
	"Name" varchar(255) NOT NULL,

	CONSTRAINT "PK_Languages"
		PRIMARY KEY ("LanguageId")
);

CREATE TABLE "ContentElements"
(
	"ContentElementId" serial NOT NULL,
	"ContentType" integer NOT NULL,
	"DefaultLanguage" varchar(10) NOT NULL,

	CONSTRAINT "PK_ContentElements"
		PRIMARY KEY ("ContentElementId"),
	CONSTRAINT "FK_ContentElements_Languages"
		FOREIGN KEY ("DefaultLanguage")
		REFERENCES "Languages"("LanguageId")
);

CREATE TABLE "TextContents"
(
	"TextContentId" serial NOT NULL,
	"ContentElementId" integer NOT NULL,
	"ContentStatus" integer NOT NULL,
	"Language" varchar(10) NOT NULL,
	"Value" text,

	CONSTRAINT "PK_TextContents"
		PRIMARY KEY ("TextContentId"),
	CONSTRAINT "FK_TextContents_ContentElements"
		FOREIGN KEY ("ContentElementId")
		REFERENCES "ContentElements"("ContentElementId"),
	CONSTRAINT "FK_TextContents_Languages"
		FOREIGN KEY ("Language")
		REFERENCES "Languages"("LanguageId")
);

CREATE TABLE "Documents"
(
	"DocumentId" serial NOT NULL,
	"Name" varchar(255) NOT NULL,
	"Status" integer NOT NULL,

	CONSTRAINT "PK_Documents"
		PRIMARY KEY ("DocumentId")
);

CREATE TABLE "Sections"
(
	"SectionId" serial NOT NULL,
	"Type" int NOT NULL,
	"Name" varchar(255) NOT NULL,
	"DocumentId" int NOT NULL,

	CONSTRAINT "PK_Sections"
		PRIMARY KEY ("SectionId"),
	CONSTRAINT "FK_Sections_Documents"
		FOREIGN KEY ("DocumentId")
		REFERENCES "Documents"
);

CREATE TABLE "SectionsContentElements"
(
	"SectionId" int NOT NULL,
	"ContentElementId" int NOT NULL,

	CONSTRAINT "PK_SectionsContentElements"
		PRIMARY KEY ("SectionId", "ContentElementId"),
	CONSTRAINT "FK_SectionsContentElements_ContentElements"
		FOREIGN KEY ("ContentElementId")
		REFERENCES "ContentElements"("ContentElementId"),
	CONSTRAINT "FK_SectionsContentElements_Sections"
		FOREIGN KEY ("SectionId")
		REFERENCES "Sections"("SectionId")
);