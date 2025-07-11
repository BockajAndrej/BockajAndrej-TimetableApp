# TimeTableApp


### Scaffolding command
```bash
dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=applicationDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context MyDbContext --no-onconfiguring --data-annotations
```

## Sources
[Informations about Entity framework](https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx)
Predicates combination https://www.telerik.com/blogs/dynamic-filter-expressions-in-an-openaccess-linq-query
Predicates combination Works: https://www.answeroverflow.com/m/1186781626282299402

Lambda clausule was good but query did not work:
```
List<EmployeeDetailModel> selectedEmployeeDetails = SelectedEmployees.OfType<EmployeeDetailModel>().ToList();
List<Expression<Func<Cp, bool>>> filterExpressions = new List<Expression<Func<Cp, bool>>>();

foreach (var employee in selectedEmployeeDetails)
{
    Expression<Func<Cp, bool>> filter = c => c.IdEmployee == employee.Id;
    filterExpressions.Add(filter);
}

Func<Expression, Expression, BinaryExpression>[] operators =
    Enumerable.Repeat(
        (Func<Expression, Expression, BinaryExpression>)Expression.OrElse,
        filterExpressions.Count - 1
    ).ToArray();

predicateForCpQuery = CombinePredicates<Cp>(filterExpressions, operators);

LoadDataCpQuery();
```

```
public Expression<Func<T, bool>> CombinePredicates<T>(IList<Expression<Func<T, bool>>> predicateExpressions,
    IList<Func<Expression, Expression, BinaryExpression>> logicalFunctions)
{
    Expression<Func<T, bool>> filter = null;

    if (predicateExpressions.Count > 0)
    {
        Expression<Func<T, bool>> firstPredicate = predicateExpressions[0];
        Expression body = firstPredicate.Body;
        for (int i = 1; i < predicateExpressions.Count; i++)
        {
            body = logicalFunctions[i - 1](body, predicateExpressions[i].Body);
        }
        filter = Expression.Lambda<Func<T, bool>>(body, firstPredicate.Parameters);
    }

    return filter;
}
```