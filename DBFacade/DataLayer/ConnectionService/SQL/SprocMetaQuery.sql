SELECT name[StoredProcedureName]
FROM INFORMATION_SCHEMA.ROUTINES[routines] 
INNER JOIN sys.procedures[sys_procs] 
on sys_procs.name = routines.SPECIFIC_NAME
WHERE ROUTINE_TYPE = 'PROCEDURE' AND sys_procs.is_ms_shipped = 0