CREATE TABLE "Language"
(
	"LanguageId" varchar(10) NOT NULL,
	"Name" varchar(255) NOT NULL,

	CONSTRAINT "PK_Language"
		PRIMARY KEY ("LanguageId")
);

CREATE TABLE "ContentElement"
(
	"ContentElementId" serial NOT NULL,
	"ContentType" integer NOT NULL,
	"DefaultLanguage" varchar(10) NOT NULL,

	CONSTRAINT "PK_ContentElement"
		PRIMARY KEY ("ContentElementId")
);

CREATE TABLE "TextContent"
(
	"ContentValueId" serial NOT NULL,
	"ContentElementId" integer NOT NULL,
	"ContentStatus" integer NOT NULL,
	"Language" varchar(10) NOT NULL,
	"Value" text,

	CONSTRAINT "PK_TextContent"
		PRIMARY KEY ("ContentValueId"),
	CONSTRAINT "FK_TextContent_ContentElement"
		FOREIGN KEY ("ContentElementId")
		REFERENCES "ContentElement"("ContentElementId")
);