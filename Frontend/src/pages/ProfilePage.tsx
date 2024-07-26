import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';
import CreateOutlinedIcon from '@mui/icons-material/CreateOutlined';
import GenericPage from "./GenericPage";
import { FC } from "react";
import ProfileHeader from "../components/profile/profileHeader";
import { Box, Container } from '@mui/material';
import { Gender } from '../shared/enums/gender';
import { SubscriptionType } from '../shared/enums/subscriptionType';
import { UserWithSubscription } from '../shared/interfaces/user';
import ProfileDescription from '../components/profile/profileDescription';
import ProfileGallery from '../components/profile/profileGallery';
import ProfileSubscription from '../components/profile/profileSubscription';

const user: UserWithSubscription = {
  id: "user-123",
  fusionUserId: "fusion-456",
  firstName: "JohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn",
  lastName: "DoeJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn",
  age: 30,
  city: "New YorkJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn",
  description: "Software developer and tech enthusiastJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn.",
  gender: Gender.Man,
  email: "john.doe@example.comJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohnJohn",
  photos: [
    {
      id: "photo-789",
      isAvatar: true,
      photoURL: "https://randomuser.me/api/portraits/men/3.jpg",
      userId: "user-123"
    },
    {
      id: "photo-790",
      isAvatar: false,
      photoURL: "https://randomuser.me/api/portraits/men/4.jpg",
      userId: "user-123"
    },
    {
      id: "photo-790",
      isAvatar: false,
      photoURL: "https://randomuser.me/api/portraits/men/4.jpg",
      userId: "user-123"
    },
    {
      id: "photo-790",
      isAvatar: false,
      photoURL: "https://randomuser.me/api/portraits/men/4.jpg",
      userId: "user-123"
    },
  ],
  subscription: {
    id: "subscription-001",
    email: "john.doe@example.com",
    subscriptionType: SubscriptionType.Premium,
    expiresAt: new Date('2025-07-26'),
    createdAt: new Date('2024-07-26'),
    updatedAt: new Date()
  }
};

const ProfilePage: FC = () => {
  return (
    <GenericPage title="Profile" icons={[<SettingsOutlinedIcon/>, <CreateOutlinedIcon/>]}>
      <Container maxWidth="sm" sx={{ margin:"32px 0",
            '@media (min-width:600px)': {
            padding: 0
        }}}>
          <Box sx={{ backgroundColor: '#fff', padding: 2, borderRadius: "24px", boxShadow: 3 }}>
            <ProfileHeader firstName={user.firstName} email={user.email} photos={user.photos} />
            <ProfileGallery photos={user.photos} />
            <ProfileDescription description={user.description} city={user.city} age={user.age} email={user.email} gender={user.gender} />
            <ProfileSubscription subscription={user.subscription}/>
          </Box>
      </Container>
    </GenericPage>
  );
};

export default ProfilePage;