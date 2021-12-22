{ Avaerage out a series of numbers }
{ enter one number per line and terminate with a zero }

var total, count, current;
  begin
    total := 0; { total of all non-zero values read so far }
    count := 0; { count of values read }
    current := read;

    while current <> 0 do
    begin
        total := total + current;
        count := count + 1;
        current := read
    end;

    write(count);
    write(total);

    if count <> 0 then 
        write(total / count); { the average valiue }
end.
