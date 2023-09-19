insert into Containers
(Code, Length, Type)
values 
('FOOD', 3, 'food'),
('FOOD', 1, 'food'),
('ELEC', 2, 'electronics'),
('ELEC', 1, 'electronics'),
('PSNS', 1, 'poisonous'),
('FLMB', 3, 'flammable'),
('PSNS', 3, 'poisonous');



insert into Spots
(X, Y, ContainerId)
values
(1, 1, null),
(1, 2, null),
(1, 3, 4),
(1, 4, null),
(1, 5, null),

(2, 1, 5),
(2, 2, null),
(2, 3, 7),
(2, 4, 7),
(2, 5, 7),

(3, 1, null),
(3, 2, null), 
(3, 3, null), 
(3, 4, null), 
(3, 5, null), 

(4, 1, null),
(4, 2, null),
(4, 3, null),
(4, 4, null),
(4, 5, null),

(5, 1, null),
(5, 2, null),
(5, 3, null),
(5, 4, null),
(5, 5, null);

