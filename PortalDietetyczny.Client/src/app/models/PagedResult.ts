export class PagedResult<T>
{
  pageNumber: number;
  totalCount: number;
  data: T[];
}
