export class AddTagDto
{
  Name: string
  context: TagContext
}

export enum TagContext {
  Recipe = 0,
  Diet = 1
}
