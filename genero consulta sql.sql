select distinct g.Nombre from GENERO as g 
INNER JOIN  GENERO_PRODUCTO as gp on g.IdGenero = gp.Fk_Genero
INNER JOIN  PRODUCTO as p on gp.Fk_Producto = '7'


select g.Nombre from GENERO as g,  GENERO_PRODUCTO as gp, PRODUCTO as p 
WHERE gp.Fk_Producto = p.IdProducto 
and g.IdGenero = gp.Fk_Genero
and p.IdProducto = '6'