import { UserData } from "./packet";
import { Server } from "./server";
import { User } from "./user";

export class Lobby {
  private users: Map<number, User>;
  public userNames: Set<string>;

  public constructor(_server: Server) {
    this.users = new Map<number, User>();
    this.userNames = new Set<string>();
  }

  public addUser(user: User) {
    console.log("addUser " + JSON.stringify(user.getUserData()));

    this.broadcast((playerId, player) => player.sendEnterUser(user));
    for (const [id, player] of this.users) {
      user.sendEnterUser(player);
    }

    user.sendEnterUser(user);
    this.userNames.add(user.getDisplayName());
    this.users.set(user.getUserId(), user);
  }

  public removeUser(user: User) {
    this.broadcast((playerId, player) => player.sendRemoveUser(user));
    this.userNames.delete(user.getDisplayName());
    this.users.delete(user.getUserId());
  }

  public getUser(userId: number) {
    return this.users.get(userId);
  }

  public chat(sender: User, message: string) {
    this.broadcast((playerId, player) => {
      player.sendChat(sender.getUserId(), sender.getUserData().displayName, message);
    });
  }

  public privateChat(sender: User, message: string, receiverId: number) {
    let user = this.getUser(receiverId);
    user?.sendChat(sender.getUserId(), sender.getUserData().displayName, "(private) " + message);
    sender.sendChat(sender.getUserId(), sender.getUserData().displayName, "(private) " + message);
  }

  private broadcast(handler: (playerId: number, player: User) => void) {
    for (const [id, player] of this.users) {
      if (player !== undefined) {
        handler(id, player);
      }
    }
  }

  public close() {
    this.users.clear();
  }
}
