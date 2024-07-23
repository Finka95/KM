import { FC } from "react"
import { AppBar, Toolbar, Container} from "@mui/material";
import FavoriteIcon from '@mui/icons-material/Favorite';
import NearMeIcon from '@mui/icons-material/NearMe';
import ChatIcon from '@mui/icons-material/Chat';
import AccountBoxIcon from '@mui/icons-material/AccountBox';
import NavbarItem from "../navbarItem/navbarItem";
import StyleIcon from '@mui/icons-material/Style';

const Footer: FC = () => {
  return (
    <AppBar position="sticky" color="secondary" sx={{bottom: 0}}>
      <Container 
        maxWidth="sm"
        sx={{ 
          backgroundColor: "#FFFFFF",
          color: "#000000"
        }}>
        <Toolbar 
          sx={{ 
            justifyContent: 'space-between',
            padding: "0"
          }}>
          <NavbarItem to="/near" text="Near" icon={<NearMeIcon />} ></NavbarItem>
          <NavbarItem to="/" text="Hit or miss" icon={<StyleIcon />} ></NavbarItem>
          <NavbarItem to="/likes" text="Likes" icon={<FavoriteIcon />} ></NavbarItem>
          <NavbarItem to="/chats" text="Chats" icon={<ChatIcon />} ></NavbarItem>
          <NavbarItem to="/profile" text="Profile" icon={<AccountBoxIcon />} ></NavbarItem>
        </Toolbar>
      </Container>
    </AppBar>
  )
}

export default Footer;