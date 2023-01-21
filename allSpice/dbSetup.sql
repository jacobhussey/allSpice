CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(50) NOT NULL,
        instructions TEXT NOT NULL,
        img VARCHAR(255) NOT NULL,
        category VARCHAR(25) NOT NULL DEFAULT 'misc',
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    recipes (
        title,
        instructions,
        `img`,
        category,
        `creatorId`
    )
VALUES (
        'test',
        'this is a test',
        'https://images.unsplash.com/photo-1575485670541-824ff288aaf8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1074&q=80',
        'misc',
        '63c869f835ad998053cddff4'
    );

SELECT re.*, ac.*
FROM recipes re
    JOIN accounts ac ON ac.id = re.creatorId;

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(55) NOT NULL,
        quantity VARCHAR(55) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes (id) ON DELETE CASCADE
    ) default charset utf8;

INSERT INTO
    ingredients (name, quantity, `recipeId`)
VALUES ('test', '5 cups of coffee', 1);