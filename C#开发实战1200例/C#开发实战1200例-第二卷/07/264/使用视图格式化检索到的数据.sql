--������ͼ
create view v_warebooks
as 
--����SELECT��ѯ��䲢��ʽ������
select ͼ������,ͼ�����,��������,
       convert(varchar(10) ,
       cast(�������� as smalldatetime),120)as ��ʽ������
from tb_Csharpbook
go
--��ѯ����������ͼ�е�����
select * from v_warebooks
