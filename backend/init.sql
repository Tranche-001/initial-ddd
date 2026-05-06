-- Init script: runs once on first container startup
CREATE TABLE IF NOT EXISTS "Users" (
    "Id"    UUID          NOT NULL DEFAULT gen_random_uuid() PRIMARY KEY,
    "Name"  VARCHAR(255)  NOT NULL,
    "Email" VARCHAR(255)  NOT NULL UNIQUE
);
