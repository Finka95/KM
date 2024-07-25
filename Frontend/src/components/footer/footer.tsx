import { FC } from "react"
import { AppBar, Toolbar, Container} from "@mui/material";
import NavbarItem from "../navbarItem/navbarItem";
import NavigationOutlinedIcon from '@mui/icons-material/NavigationOutlined';
import StyleOutlinedIcon from '@mui/icons-material/StyleOutlined';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';
import ChatOutlinedIcon from '@mui/icons-material/ChatOutlined';
import PermIdentityOutlinedIcon from '@mui/icons-material/PermIdentityOutlined';

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
          <NavbarItem to="/near" text="Near" icon={<NavigationOutlinedIcon />} ></NavbarItem>
          <NavbarItem to="/" text="Hit or miss" icon={<StyleOutlinedIcon />} ></NavbarItem>
          <NavbarItem to="/likes" text="Likes" icon={<FavoriteBorderOutlinedIcon />} ></NavbarItem>
          <NavbarItem to="/chats" text="Chats" icon={<ChatOutlinedIcon />} ></NavbarItem>
          <NavbarItem to="/profile" text="Profile" icon={<PermIdentityOutlinedIcon />} ></NavbarItem>
        </Toolbar>
      </Container>
    </AppBar>
  )
}

export default Footer;