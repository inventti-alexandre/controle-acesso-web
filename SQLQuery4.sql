SELECT *  
                         FROM   dbo.pessoas a  
                         WHERE  EXISTS(SELECT 1  
                                      FROM   dbo.cursopresencas b 
                                              INNER JOIN dbo.cursos c  
                                                     ON c.cursoid = b.cursoid    

                                   WHERE  c.eventoid = 1
								   and b.PessoaID = a.PessoaID)  