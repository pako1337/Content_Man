INSERT INTO "Languages" ("LanguageId", "Name") VALUES
	('en', 'English'),
	('pl', 'Polish'),
	('Invariant', 'Invariant');


INSERT INTO "ContentElements" ("ContentType", "DefaultLanguage") VALUES
	(1, 'en'),
	(1, 'pl');

INSERT INTO "TextContents" ("ContentElementId", "ContentStatus", "Language", "Value") VALUES
	(1, 1, 'en', 'english sample'),
	(1, 1, 'pl', 'polski przykład'),
	(1, 1, 'Invariant', 'Invariant sample'),
	(2, 1, 'pl', 'Litwo, ojczyzno moja'),
	(2, 1, 'en', 'O Lithuania, my country');