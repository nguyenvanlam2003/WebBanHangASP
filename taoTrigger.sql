create trigger update_gioHang
on ChiTietGioHangs for insert, update, delete
as
begin
    update gh
    set tongTien = (select SUM(ct.thanhTien) 
                    from ChiTietGioHangs ct 
                    where ct.maGH = gh.maGH),
        soLuong = (select COUNT(ct.maSP) 
                   from ChiTietGioHangs ct 
                   where ct.maGH = gh.maGH)
    from GioHangs gh
    where gh.maGH in (select maGH from inserted)
       or gh.maGH in (select maGH from deleted);
end;
drop trigger  update_gioHang
go
create trigger update_CTgioHang
on ChiTietGioHangs for insert, update
as
begin
  update ChiTietGioHangs 
  set  gia=ISNULL(sp.giaMoi, sp.giaBan),
    thanhTien=ct.soLuong * ISNULL(sp.giaMoi, sp.giaBan)
   from ChiTietGioHangs ct
    inner join inserted i on ct.ID = i.ID
    inner join SanPhams sp on ct.maSP = sp.maSP;
end
go
drop trigger  update_CTgioHang